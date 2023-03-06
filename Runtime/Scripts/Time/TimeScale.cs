using Codice.Client.BaseCommands;

namespace Common.Runtime.Time
{
    public class TimeScale
    {
        public static UnityTimeScale Unity = new UnityTimeScale();

        public virtual float Scale
        {
            get => _scale;
            set => _scale = value;
        }
        public virtual float DeltaTime => UnityEngine.Time.deltaTime * _scale;
        public virtual float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime * _scale;
        public virtual float Time => UnityEngine.Time.time * _scale;

        private float _scale;
        
        public TimeScale(float scale = 1f)
        {
            _scale = scale;
        }
    }
}