using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Inhterit from and put in an empty object, preferably "Game Manager".

public abstract class GameManagerBase : Singleton<GameManagerBase>
{
    public Events.EventGameState OnGameStateChanged;


    [SerializeField] protected GameObject[] SystemPrefabs;
    protected List<GameObject> _instancedSystemPrefabs = new List<GameObject>();
    protected List<AsyncOperation> _loadOperations = new List<AsyncOperation>();

    protected string _currentLevelName = string.Empty;

    public enum GameState
    {
        PREGAME,
        RUNING,
        PAUSED
    }

    public GameState CurrentGameState { get; private set; } = GameState.PREGAME;

    protected virtual void UpdateState(GameState state)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(CurrentGameState, previousGameState);
    }


    protected virtual void Start()
    {
        DontDestroyOnLoad(gameObject);
        InstantiateSystemPrefabs();
        UIManagerBase.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    protected virtual void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadLevel(_currentLevelName);
        }
    }

    protected virtual void Update()
    {
        if (CurrentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    protected virtual void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (loadScene == null)
        {
            Debug.LogError("[GameManager] Unable to load level" + levelName);
            return;
        }

        loadScene.completed += LoadScene_completed;
        _loadOperations.Add(loadScene);
        _currentLevelName = levelName;

    }

    protected virtual void LoadScene_completed(AsyncOperation obj)
    {
        if (_loadOperations.Contains(obj))
        {
            _loadOperations.Remove(obj);

            if (_loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNING);

            }
        }

    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(levelName);
        if (unloadScene == null)
        {
            Debug.LogError("[GameManager] Unable to unload level" + levelName);
            return;
        }
        unloadScene.completed += UnloadScene_completed;
    }

    protected virtual void UnloadScene_completed(AsyncOperation obj)
    {
        Debug.Log("Unload Complete");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }

    public void StartGame(string startingScene)
    {

        LoadLevel(startingScene);
    }
    public void TogglePause()
    {
        if (CurrentGameState == GameState.RUNING)
        {
            UpdateState(GameState.PAUSED);
        }
        else
        {
            UpdateState(GameState.RUNING);
        }
    }

    public void RestartGame()
    {

        //TODO There is a bug, if you reload a map, before unloading last, singletons dublicate.
        UpdateState(GameState.PREGAME);
    }
    public void QuitGame()
    {
        //TODO: Implement features for quiting game (autosave etc.)
        Application.Quit();
    }
}
