using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeCamera;
    [SerializeField] private UiCards uiCards;
    [SerializeField] private string upgradeUiText;
    
    public Container container;
    public List<MenuData> appliedUpgrades;
    public List<MenuData> purchasedUpgrades;

    private void Awake()
    {
        LevelSystem.ExperienceChanged += OnExperienceChanged;
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Mission)
        {
            appliedUpgrades.Clear();
            StartCoroutine(ApplyPurchased());
        }
    }

    private IEnumerator ApplyPurchased()
    {
        yield return null;
        
        foreach (var data in purchasedUpgrades)
        {
            data.Apply(gameObject);
            appliedUpgrades.Add(data);
        }
    }

    private void OnExperienceChanged(float arg1, float arg2, int arg3, bool leveled)
    {
        if (leveled)
        {
            GenerateUpgrades();
        }
    }

    private void GenerateUpgrades()
    {
        var possibleUpgrades = container.GetUpgrades().Where(x => x.CanShow(gameObject)).ToList();

        var cardsData = new List<UiCards.CardData>();

        if (possibleUpgrades.Count <= 3)
        {
            foreach (var upgrade in possibleUpgrades)
            {
                cardsData.Add(GenerateCardData(upgrade));
            }
        }
        else
        {
            var numbers = GenerateDistinctRandomNumbers(3, 0, possibleUpgrades.Count - 1);
            foreach (var number in numbers)
            {
                var selectedUpgrade = possibleUpgrades[number];
                cardsData.Add(GenerateCardData(selectedUpgrade));
            }
        }

        uiCards.ShowButtons(upgradeUiText, cardsData, () =>
            {
                TimeManager.StopTime();
                upgradeCamera.SetActive(true);
            }, () =>
            {
                TimeManager.ResumeTime();
                upgradeCamera.SetActive(false);
            }
        );
    }

    private UiCards.CardData GenerateCardData(MenuData data)
    {
        var cardData = new UiCards.CardData(
            data.GetTitle(),
            data.GetDescription(),
            data.GetCost(),
            gameObject,
            _ => data.CanShow(gameObject),
            () =>
            {
                data.Apply(gameObject);
                appliedUpgrades.Add(data);
            },
            data.LoopInMenu()
        );
        return cardData;
    }

    private static IEnumerable<int> GenerateDistinctRandomNumbers(int count, int min, int max)
    {
        var numbers = Enumerable.Range(min, max - min + 1).ToArray();

        for (var i = 0; i < numbers.Length; i++)
        {
            var temp = numbers[i];
            var randomIndex = Random.Range(i, numbers.Length);
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        return numbers.Take(count).ToArray();
    }

    private void OnDestroy()
    {
        LevelSystem.ExperienceChanged -= OnExperienceChanged;
        GameManager.GameStateChanged -= OnGameStateChanged;
    }
}