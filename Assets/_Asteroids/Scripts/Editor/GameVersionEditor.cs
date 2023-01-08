using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CustomEditor(typeof(GameVersion))]
public class GameVersionEditor : Editor, IPreprocessBuildWithReport
{
    [SerializeField] private GameVersion gameVersion;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        gameVersion = (GameVersion)target;
        
        if(GUILayout.Button("Generate version"))
        {
            gameVersion.GenerateVersion();
        }
    }

    public int callbackOrder { get; }
    public void OnPreprocessBuild(BuildReport report)
    {
        if (gameVersion == null) return;
        var version = gameVersion.GenerateVersion();
        PlayerSettings.bundleVersion = version;
        
#if UNITY_ANDROID
        var android = gameVersion.GenerateAndroidVersion();
        PlayerSettings.Android.bundleVersionCode = android;
#endif
        var textEditor = new TextEditor
        {
            text = android.ToString()
        };
        textEditor.SelectAll();
        textEditor.Copy();
    }
}