namespace BananaParty.Arch.TestUtilities
{
    public static class TestRunnerApplication
    {
        public static int FirstSceneIndex
        {
            get
            {
#if UNITY_EDITOR
                // Normal behavior in the editor
                return 0;
#else
                // In a build test run, scene 0 is the test runner bootstrap,
                // you would get stuck in an infinite loop when loading scene index 0
                return 1;
#endif
            }
        }
    }
}
