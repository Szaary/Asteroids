using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject menuCamera;
    [SerializeField] private GameObject endBattle;
    private void Awake()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        menuCamera.SetActive(state == GameState.Menu);
        endBattle.SetActive(state is GameState.Victory or GameState.Defeat);
    }
    
    
    private void OnDestroy()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }
}
