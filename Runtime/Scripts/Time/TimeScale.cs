using Codice.Client.BaseCommands;

namespace Common.Runtime.Time
{
    public class TimeScale
    {
        public static UnityTimeScale UnityScale = new UnityTimeScale();

        public virtual float Scale
        {
            get => _scale;
            set => _scale = value;
        }
        public virtual float DeltaTime => UnityEngine.Time.deltaTime * Scale;
        public virtual float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime * Scale;
        
        private float _scale;
        
        public TimeScale(float scale = 1f)
        {
            _scale = scale;
        }
    }
}