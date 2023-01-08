using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }
    public static bool IsInitialized => Instance != null;

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("[Singleton] Trying to instantiate a second instance of a singleton class: " + Instance.name);
        }
        else
            Instance = (T) this;
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
