using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    // TO TURN ON MENU on each level include this in empty object that wrap entire scene.
    void Start()
    {
        GameManagerBase.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleGameStateChanged(GameManagerBase.GameState currentState, GameManagerBase.GameState previousState)
    {
        if (currentState == GameManagerBase.GameState.PAUSED)
        {
            gameObject.SetActive(false);
        }
        if (currentState == GameManagerBase.GameState.PREGAME)
        {
            gameObject.SetActive(false);
        }
        if (currentState == GameManagerBase.GameState.RUNING)
        {
            gameObject.SetActive(true);            
        }
    }
}
