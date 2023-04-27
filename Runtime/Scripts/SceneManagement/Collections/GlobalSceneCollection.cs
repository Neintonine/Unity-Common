using System.Collections.Generic;
using System.Linq;
using SceneManagement.Runtime;
using UnityEditor;
using UnityEngine;

namespace Common.Runtime.SceneManagement.Collections
{
    public sealed class GlobalSceneCollection : SingletonScriptableObject<GlobalSceneCollection>
    {
        public SceneReference PersistentScene => this._persistentScene;
        public SceneCollection StartCollection => this._startCollection;
        public List<SceneReference> DefaultScene => this._defaultScenes;
        
        [SerializeField] private SceneReference _persistentScene;
        [SerializeField] private SceneCollection _startCollection;
        [SerializeField] private List<SceneReference> _defaultScenes;


        public IEnumerable<string> GetCombinedScenes(ILoadingCollection loadingManager)
        {
            return loadingManager.IgnoreDefaultScenes ? loadingManager.GetScenes() : this._defaultScenes.Select(a => a.ScenePath).Concat(loadingManager.GetScenes());
        }
    }
}