// This is a copy of WaitWhile.cs from Unity 6, to make it available in Unity 2022.3.
// Slightly renamed to avoid naming conflicts.

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BananaParty.Arch.TestUtilities.Polyfills
{
    /// <summary>
    /// Suspends the coroutine execution until the supplied delegate evaluates to false.
    /// </summary>
    public sealed class TimedWaitWhile : CustomYieldInstruction
    {
        private readonly Func<bool> m_Predicate;

        private readonly Action m_TimeoutCallback;

        private readonly TimeoutMode m_TimeoutMode;

        private readonly double m_MaxExecutionTime = -1.0;

        public override bool keepWaiting
        {
            get
            {
                if (m_MaxExecutionTime == -1.0)
                {
                    return m_Predicate();
                }

                if (GetTime() > m_MaxExecutionTime)
                {
                    m_TimeoutCallback();
                    return false;
                }

                return m_Predicate();
            }
        }

        public TimedWaitWhile(Func<bool> predicate, TimeSpan timeout, Action onTimeout, TimeoutMode timeoutMode = TimeoutMode.Realtime)
        {
            m_Predicate = predicate;

            if (timeoutMode == TimeoutMode.InGameTime && !Application.isPlaying)
            {
                throw new ArgumentException("InGameTime mode is not supported in Editor in edit mode", "timeoutMode");
            }

            if (timeout <= TimeSpan.Zero)
            {
                throw new ArgumentException("Timeout must be greater than zero", "timeout");
            }

            m_TimeoutCallback = onTimeout ?? throw new ArgumentNullException("onTimeout", "Timeout callback must be specified");
            m_TimeoutMode = timeoutMode;
            m_MaxExecutionTime = GetTime() + timeout.TotalSeconds;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTime()
        {
            return (m_TimeoutMode == TimeoutMode.InGameTime) ? Time.timeAsDouble : Time.realtimeSinceStartupAsDouble;
        }
    }
}
