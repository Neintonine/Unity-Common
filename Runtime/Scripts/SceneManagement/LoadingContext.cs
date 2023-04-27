namespace Common.Runtime.SceneManagement
{
    public class LoadingContext
    {
        public enum LoadingState
        {
            SceneLoading,
            LoadingEvent
        }
        
        public class SceneLoadingState
        {
            public int ProcessesCompleted;
            public int ProcessesCount;
            
            public float Progress;
        }
        
        public class LoadingEventState
        {
            public int EventsCompleted;
            public int EventCount;

            public LoadingEventContext CurrentEventContext;
            public float Progress;
        }

        public LoadingState State = LoadingState.SceneLoading;
        
        public SceneLoadingState SceneState = new SceneLoadingState();
        public LoadingEventState EventState = new LoadingEventState();
    }
}