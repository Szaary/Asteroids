using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Inherit from it
// To start game override update and call function start game with scene name as parameter.

//protected override void Update()
//{
//    base.Update();     
//    StartGame(" scene name ");
//}

public abstract class UIManagerBase : Singleton<UIManagerBase>
{
    [SerializeField] protected MainMenuBase _mainMenu;
    [SerializeField] protected PauseMenuBase _pauseMenu;

    [SerializeField] protected Camera _camera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    protected virtual void Start()
    {
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        GameManagerBase.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    protected virtual void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete?.Invoke(fadeOut);
    }

    protected virtual void HandleGameStateChanged(GameManagerBase.GameState currentState, GameManagerBase.GameState previousState)
    {
        _pauseMenu.gameObject.SetActive(currentState == GameManagerBase.GameState.PAUSED);

        SetMenuCameraActive(currentState == GameManagerBase.GameState.PAUSED);
    }

    protected virtual void Update()
    {
        
        //     StartGame(" Scene name ");
    }

    protected virtual void StartGame(string sceneName)
    {
        if (GameManagerBase.Instance.CurrentGameState != GameManagerBase.GameState.PREGAME)
        {         
            return;
        }
        if (Input.anyKeyDown)
        {

            GameManagerBase.Instance.StartGame(sceneName);
        }
    }

    public virtual void SetMenuCameraActive(bool active)
    {
        _camera.gameObject.SetActive(active);
    }
}
