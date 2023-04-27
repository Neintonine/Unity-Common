using System;
using System.Collections.Generic;

namespace Common.Runtime.SceneManagement
{
    public sealed class LoadingEvent
    {
        internal static List<Action<LoadingEventContext>> _loadingActions = new List<Action<LoadingEventContext>>();
        internal static List<Action> _startActions = new List<Action>();

        internal static void TriggerStartActions()
        {
            foreach (Action action in LoadingEvent._startActions)
            {
                action.Invoke();
            }
        } 
        
        public static void RegisterStartAction(Action action)
        {
            LoadingEvent._startActions.Add(action);
        }

        public static void UnregisterStartAction(Action action)
        {
            LoadingEvent._startActions.Remove(action);
        }
        
        public static void RegisterLoadingAction(Action<LoadingEventContext> action)
        {
            LoadingEvent._loadingActions.Add(action);
        }

        public static void UnregisterLoadingAction(Action<LoadingEventContext> action)
        {
            LoadingEvent._loadingActions.Remove(action);
        }
    }
}