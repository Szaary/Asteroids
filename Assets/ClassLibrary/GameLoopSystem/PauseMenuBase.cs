using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PauseMenuBase : MonoBehaviour
{
    [SerializeField] protected Button _resumeButton;
    [SerializeField] protected Button _restartButton;
    [SerializeField] protected Button _quitButton;

    protected virtual void Start()
    {
        _resumeButton.onClick.AddListener(HandleResumeClicked);
        _restartButton.onClick.AddListener(HandleRestartClicked);
        _quitButton.onClick.AddListener(HandleQuitClicked);
    }
    protected virtual void HandleResumeClicked()
    {
        GameManagerBase.Instance.TogglePause();
    }
    protected virtual void HandleRestartClicked()
    {
        GameManagerBase.Instance.RestartGame();
    }
    protected virtual void HandleQuitClicked()
    {
        GameManagerBase.Instance.QuitGame();
    }
}
