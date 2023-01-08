using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private List<MenuData> menus;
    [SerializeField] private MenuData victory;
    [SerializeField] private MenuData defeat;
       
    [SerializeField] private GameObject player;


    [SerializeField] private string mainMenuText;
    [SerializeField] private string victoryText;
    [SerializeField] private string defeatText;
    
    private void Awake()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                UiCards.GenerateCards(mainMenuText, menus, player);
                break;
            case GameState.Victory:
                UiCards.GenerateCard(victoryText, victory, player);
                break;
            case GameState.Defeat:
                UiCards.GenerateCard(defeatText, defeat, player);
                break;
        }
    }

    private void OnDestroy()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
    }
}