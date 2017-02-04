using UnityEditor;
using UnityEngine;

public static class DoABuild
{
    public static void DoIt()
    {
        var target = BuildTarget.StandaloneWindows;

        if (Application.platform == RuntimePlatform.OSXEditor)
            target = BuildTarget.StandaloneOSXUniversal;

        BuildPipeline.BuildPlayer(new[] {"Assets/scenes/main.unity"}, "builds/build", target, BuildOptions.None);
    }
}
