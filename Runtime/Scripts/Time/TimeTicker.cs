using UnityEngine;

namespace Common.Runtime.Time
{
    [AddComponentMenu("_SYSTEM/Time/TimeTicker")]
    public class TimeTicker : AutoStaticMonoBehaviour<TimeTicker>
    {
        
        [Tooltip("If true, it will execute in FixedUpdate with Time.fixedDeltaTime")] public bool fixedUpdate = false;
        [Tooltip("If true, it will execute in LateUpdate instead of Update.\n(only applies when FixedUpdate = false)")] public bool lateUpdate = false;

        protected override void OnEnable()
        {
            base.OnEnable();
            Stopwatch._activeStopwatches.RemoveAll(a => !a.Keep);
        }

        // Update is called once per frame
        void Update()
        {
            if (!fixedUpdate && !lateUpdate)
                Stopwatch.PerformTicks(UnityEngine.Time.deltaTime);
        }
        private void LateUpdate()
        {
            if (!fixedUpdate && lateUpdate)
                Stopwatch.PerformTicks(UnityEngine.Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (fixedUpdate)
                Stopwatch.PerformTicks(UnityEngine.Time.fixedDeltaTime);
        }
    }
}
