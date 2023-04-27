using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Codice.CM.WorkspaceServer.DataStore.IncomingChanges;
using Common.Runtime.SceneManagement.Collections;
using Common.Runtime.SceneManagement.LoadingScreens;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace Common.Runtime.SceneManagement
{
    public sealed class LoadingManager : MonoBehaviour
    {
        private static LoadingManager _loadingManager;
        
        [SerializeField] private LoadingScreen _loadingScreen;
        
        private ILoadingCollection _lastCollection;
        private LoadingContext _currentLoadingContext;
        
        private void Awake()
        {
            LoadingManager._loadingManager = this;
            this._loadingScreen.Hide();
        }

        private async void _LoadScene(ILoadingCollection loadingCollection)
        {
            this._loadingScreen.Show();

            this._currentLoadingContext = new LoadingContext();
            LoadingEvent._loadingActions.Clear();
            LoadingEvent._startActions.Clear();
            
            List<AsyncOperation> loadingProcesses = new List<AsyncOperation>();
            if (this._lastCollection != null)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.path.Equals(GlobalSceneCollection.Instance.PersistentScene.ScenePath))
                    {
                        continue;
                    }
                    
                    loadingProcesses.Add(SceneManager.UnloadSceneAsync(scene));
                }
            }
            
            foreach (string scene in GlobalSceneCollection.Instance.GetCombinedScenes(loadingCollection))
            {
                loadingProcesses.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
            }

            this._lastCollection = loadingCollection;

            await this.HandleSceneLoadingProcess(loadingProcesses);
            await this.HandleLoadingEvents();
            
            LoadingEvent.TriggerStartActions();
            
            this._loadingScreen.Hide();
        }

        private async Task HandleSceneLoadingProcess(List<AsyncOperation> loadingProcesses)
        {
            bool sceneDone = false;
            int totalProcesses = loadingProcesses.Count;
            this._currentLoadingContext.SceneState.ProcessesCount = totalProcesses;
            while (!sceneDone)
            {
                int processesDone = 0;
                float progress = 0;
                for (int i = 0; i < totalProcesses; i++)
                {
                    AsyncOperation loadingProcess = loadingProcesses[i];
                    if (loadingProcess.isDone)
                    {
                        processesDone++;
                    }

                    progress += loadingProcess.progress;
                }
                
                float normalisedProgress = progress / totalProcesses;
                this.UpdateSceneState(processesDone, normalisedProgress);

                if (processesDone == totalProcesses)
                {
                    break;
                }
                
                await Task.Yield();
            }
        }

        private async Task HandleLoadingEvents()
        {
            
            List<Action<LoadingEventContext>> loadingActions = LoadingEvent._loadingActions;
            
            this._currentLoadingContext.State = LoadingContext.LoadingState.LoadingEvent;
            this._currentLoadingContext.EventState.EventsCompleted = 0;
            this._currentLoadingContext.EventState.EventCount = loadingActions.Count;
            
            foreach (Action<LoadingEventContext> action in loadingActions)
            {
                LoadingEventContext eventContext = new LoadingEventContext();
                eventContext.PropertyChanged += UpdateEventContext;
                this._currentLoadingContext.EventState.CurrentEventContext = eventContext;

                action(eventContext);

                while (!eventContext.IsCompleted)
                {
                    await Task.Yield();
                }
            }
        }

        private void UpdateEventContext(object sender, PropertyChangedEventArgs e)
        {
            LoadingContext.LoadingEventState eventState = this._currentLoadingContext.EventState;
            float eventWeight = 1f / eventState.EventCount;
            float progress = (eventState.EventsCompleted * eventWeight) +
                             (eventState.CurrentEventContext.Progress * eventWeight);
            eventState.Progress = progress;
            this.UpdateLoadingState();
        }

        private void UpdateSceneState(int processesDone, float progress)
        {
            this._currentLoadingContext.SceneState.ProcessesCompleted = processesDone;
            this._currentLoadingContext.SceneState.Progress = progress;
            
            this.UpdateLoadingState();
        }

        private void UpdateLoadingState()
        {
            this._loadingScreen.UpdateScreen(this._currentLoadingContext);
        }
        
        public static void LoadScene(ILoadingCollection loadingCollection)
        {
            if (LoadingManager._loadingManager != null)
            {
                LoadingManager._loadingManager._LoadScene(loadingCollection);
                return;
            }

            bool first = true;
            foreach (string scene in loadingCollection.GetScenes())
            {
                if (first)
                {
                    first = false;
                    SceneManager.LoadScene(scene);
                    continue;
                }

                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }

#if UNITY_EDITOR
        public static void LoadSceneEditor(ILoadingCollection collection)
        {
            EditorSceneManager.SaveOpenScenes();
            
            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                EditorSceneManager.CloseScene(EditorSceneManager.GetSceneAt(i), true);
            }
            
            bool first = true;
            IEnumerable<string> enumerable = GlobalSceneCollection.IsAvailable ? GlobalSceneCollection.Instance.GetCombinedScenes(collection) : collection.GetScenes();
            foreach (string scene in enumerable)
            {
                if (first)
                {
                    first = false;
                    EditorSceneManager.OpenScene(scene);
                    continue;
                }

                EditorSceneManager.OpenScene(scene, OpenSceneMode.Additive);
            }
        }
#endif
    }
}