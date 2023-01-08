using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum ProjectStatus
{
    Development,
    Live
}

[Serializable]
public enum ProjectPhase
{
    GM,
    VS,
    Alpha,
    Beta,
    RC
}

public class GameVersion : MonoBehaviour
{
    public GameVersionData data;

    [Header("Settings")] [SerializeField] private TextMeshProUGUI textMeshVersion;
    [SerializeField] private GameObject preAlfa;
    [SerializeField] private string preVersionText = "v: ";
    
    [SerializeField] private bool hideVersionInReleaseBuild;
    
    private void Awake()
    {
        if (!hideVersionInReleaseBuild) return;
        
#if DEVELOPMENT_BUILD
#else
        SceneManager.sceneLoaded += OnSceneLoaded;
#endif
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == "MainMenu")
        {
            textMeshVersion.gameObject.SetActive(true);
        }
        else if (scene.name == "SplashScreen")
        {
            textMeshVersion.gameObject.SetActive(true);
        }
        else
        {
            textMeshVersion.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        GenerateVersion();
        preAlfa.SetActive(data.projectStatus != ProjectStatus.Live);
    }

    public string GenerateVersion()
    {
        var date = DateTime.Now.ToString("yyyy.MM.dd.HH:mm");
        var version = preVersionText +
            (int) data.projectStatus + "." +
            (int) data.projectPhase + "." +
            date + " " +
            data.buildName;
        textMeshVersion.text = version;
        return version;
    }

    public int GenerateAndroidVersion()
    {
        var now = DateTime.Now;
        var text = now.ToString("yyMMddHH");
        return int.Parse(text);
    }

#endif
}