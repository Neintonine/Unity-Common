﻿#region usings

using System;

#endregion

namespace Common.Runtime.Time
{
    /// <summary>
    ///     Performs intervals.
    /// </summary>
    public class Interval : Timer
    {
        /// <inheritdoc />
        public Interval(float seconds) : base(seconds)
        {
        }

        /// <inheritdoc />
        public Interval(TimeSpan timeSpan) : base(timeSpan)
        {
        }

        /// <inheritdoc />
        protected override void Stopping()
        {
            TriggerEndAction();
            Reset();
        }
    }
}