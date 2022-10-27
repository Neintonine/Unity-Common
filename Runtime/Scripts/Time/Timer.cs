#region usings

using System;

#endregion

namespace Common.Runtime.Time
{
    /// <summary>
    ///     Timer-System
    /// </summary>
    public class Timer : Stopwatch
    {
        /// <summary>
        ///     Creates a timer with specified seconds.
        /// </summary>
        /// <param name="seconds"></param>
        public Timer(float seconds)
        {
            Target = seconds;
        }

        /// <summary>
        ///     Creates a timer with a time span.
        /// </summary>
        /// <param name="timeSpan"></param>
        public Timer(TimeSpan timeSpan)
        {
            Target = (float) timeSpan.TotalSeconds;
        }

        /// <summary>
        ///     The target time in seconds.
        /// </summary>
        public float Target { get; set; }

        /// <summary>
        ///     The already elapsed time but normalized to the target.
        /// </summary>
        public float ElapsedNormalized { get; private set; }

        /// <summary>
        ///     The event, that is triggered when the timer stops.
        /// </summary>
        public event Action<Timer> End;

        /// <inheritdoc />
        public override void Start(bool autoTick = true)
        {
            base.Start(autoTick);
            Reset();
        }

        private protected override void Ticking(float time)
        {
            base.Ticking(time);

            ElapsedNormalized = Math.Min(Elapsed / Target, 1);
            if (ElapsedNormalized >= 1) Stopping();
        }

        /// <summary>
        ///     Occurs, when the timer tries to stop.
        /// </summary>
        protected virtual void Stopping()
        {
            TriggerEndAction();
            Stop();
        }

        /// <summary>
        ///     This will trigger <see cref="End" />
        /// </summary>
        /// <param name="context"></param>
        protected void TriggerEndAction()
        {
            End?.Invoke(this);
        }
    }
}