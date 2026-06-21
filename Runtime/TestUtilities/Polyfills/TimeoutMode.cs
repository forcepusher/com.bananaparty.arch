// This is a copy of WaitTimeoutMode.cs from Unity 6, to make it available in Unity 2022.3.
// Slightly renamed to avoid naming conflicts.

namespace BananaParty.Arch.TestUtilities.Polyfills
{
    /// <summary>
    /// Determines which mode of time measurement to use in wait operations.
    /// </summary>
    public enum TimeoutMode
    {
        /// <summary>
        /// Evaluates timeout values as units of real time.
        /// </summary>
        Realtime,
        /// <summary>
        /// Evaluates timeout values as units of in-game time, which is scaled according to the value of Time.timeScale.
        /// </summary>
        InGameTime
    }
}
