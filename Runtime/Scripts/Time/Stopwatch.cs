#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Common.Runtime.Time
{
    /// <summary>
    ///     Represents a stopwatch.
    /// </summary>
    public class Stopwatch
    {
        internal static readonly List<Stopwatch> _activeStopwatches = new List<Stopwatch>();
        private bool _paused;
        private bool _autoTick;

        /// <summary>
        ///     If true, the stopwatch was started.
        ///     <para>This doesn't changed when paused.</para>
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        ///     Gets/Sets if the stopwatch is paused.
        /// </summary>
        public bool Paused
        {
            get => _paused;
            set
            {
                if (value)
                    Pause();
                else
                    Resume();
            }
        }

        /// <summary>
        ///     If true, the stopwatch is active and not paused... (who would have guessed...)
        /// </summary>
        public bool Running => Active && !Paused;

        /// <summary>
        ///     Contains how much time already has passed. (in seconds)
        /// </summary>
        public float Elapsed { get; protected set; }

        /// <summary>
        ///     Contains the TimeSpan of how much time already passed.
        /// </summary>
        public TimeSpan ElapsedSpan { get; protected set; }

        public bool Keep { get; set; }

        /// <summary>
        /// This event gets triggered every tick.
        /// </summary>
        public event Action<Stopwatch> Tick;

        /// <summary>
        ///     Starts the stopwatch.
        /// </summary>
        public virtual void Start(bool autoTick = true)
        {
            if (Active) return;
            if (!TimeTicker.IsUsable) TimeTicker.Create();

            if (autoTick) _activeStopwatches.Add(this);
            Active = true;
            _autoTick = autoTick;
        }


        /// <summary>
        ///     Performs a tick.
        /// </summary>
        /// <param name="context"></param>
        private protected virtual void Ticking(float time)
        {
            Elapsed += time;
            ElapsedSpan = TimeSpan.FromSeconds(Elapsed);

            Tick?.Invoke(this);
        }

        public void Update()
        {
            if (_autoTick || !Running) return;
            Ticking(time: UnityEngine.Time.deltaTime);
        }

        public void Update(float deltatime)
        {
            if (_autoTick || !Running) return;
            Ticking(deltatime);
        }

        /// <summary>
        ///     Resumes the timer.
        /// </summary>
        protected virtual void Resume()
        {
            _paused = false;
        }

        /// <summary>
        ///     Pauses the timer.
        /// </summary>
        protected virtual void Pause()
        {
            _paused = true;
        }

        /// <summary>
        ///     Stops the stopwatch.
        /// </summary>
        public virtual void Stop()
        {
            if (!Active) return;

            Active = false;

            if (_autoTick) _activeStopwatches.Remove(this);
        }

        /// <summary>
        ///     Resets the stopwatch.
        /// </summary>
        public void Reset()
        {
            Elapsed = 0;
        }

        internal static void PerformTicks(float time)
        {
            foreach(Stopwatch watch in _activeStopwatches.ToArray())
            {
                if (watch.Paused) continue;
                watch.Ticking(time);
            }
        }
    }
}
