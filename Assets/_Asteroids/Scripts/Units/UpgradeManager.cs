using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeCamera;
    [SerializeField] private UiCards uiCards;
    [SerializeField] private HUD hud;
    public List<UpgradeData> allUpgrades;
    public List<UpgradeData> appliedUpgrades;

    private void Awake()
    {
        LevelSystem.ExperienceChanged += OnExperienceChanged;
    }

    private void OnExperienceChanged(float arg1, float arg2, int arg3, bool leveled)
    {
        if (leveled)
        {
            GenerateUpgrades();
        }
    }


    public void GenerateUpgrades()
    {
        Debug.Log("Generating upgrades");
        TimeManager.StopTime();
        upgradeCamera.SetActive(true);
        var possibleUpgrades = allUpgrades.Where(x => x.CanShow(gameObject)).ToList();

        var cardsData = new List<UiCards.CardData>();

        if (possibleUpgrades.Count <= 3)
        {
            foreach (var upgrade in possibleUpgrades)
            {
                var data = GenerateCardData(upgrade);
                cardsData.Add(data);
            }
            uiCards.ShowButtons(cardsData);
        }
        else
        {
            var numbers = GenerateDistinctRandomNumbers(3, 0, possibleUpgrades.Count - 1);
            foreach (var number in numbers)
            {
                var selectedUpgrade = possibleUpgrades[number];
                var data = GenerateCardData(selectedUpgrade);
                cardsData.Add(data);
            }
        }

        uiCards.ShowButtons(cardsData);
    }

    private UiCards.CardData GenerateCardData(UpgradeData selectedUpgrade)
    {
        var data = new UiCards.CardData(
            selectedUpgrade.upgradeText,
            selectedUpgrade.description,
            () =>
            {
                selectedUpgrade.ApplyUpgrade(gameObject);
                TimeManager.ResumeTime();
                upgradeCamera.SetActive(false);
                appliedUpgrades.Add(selectedUpgrade);
            }
        );
        return data;
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
    }
}