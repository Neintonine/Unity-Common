namespace Common.Runtime.Time
{
    public sealed class UnityTimeScale : TimeScale
    {
        public static float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public static float UnscaledFixedDeltaTime => UnityEngine.Time.fixedUnscaledDeltaTime;
        public static float Time => UnityEngine.Time.time;
        public static float FixedTime => UnityEngine.Time.fixedTime;
        
        public override float Scale
        {
            get => UnityEngine.Time.timeScale;
            set => UnityEngine.Time.timeScale = value;
        }
        public override float DeltaTime => UnityEngine.Time.deltaTime;
        public override float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime;
    }
}