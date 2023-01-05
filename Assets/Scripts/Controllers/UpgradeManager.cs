using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Button> upgradeCards;
    [SerializeField] private GameObject upgradeCamera;
    [SerializeField] private List<GameObject> uiToDisable;
    
    public List<UpgradeData> allUpgrades;
    public List<UpgradeData> appliedUpgrades;

    
    public void GenerateUpgrades()
    {
        Debug.Log("Generating upgrades");
        TimeManager.StopTime();
        upgradeCamera.SetActive(true);
        var possibleUpgrades = allUpgrades.Where(x => x.CanShow(gameObject)).ToList();
        
        if (possibleUpgrades.Count <= 3)
        {
            for (var index = 0; index < upgradeCards.Count; index++)
            {
                var card = upgradeCards[index];

                if (possibleUpgrades.Count() > index)
                {
                    ShowButton(index);
                    var selectedUpgrade = possibleUpgrades[index];
                    SetButton(card, selectedUpgrade);
                }
                else
                {
                    HideButton(card.gameObject);
                }

            }
        }
        else
        {
            var numbers = GenerateDistinctRandomNumbers(3, 0, possibleUpgrades.Count-1);
            for (var index = 0; index < upgradeCards.Count; index++)
            {
                ShowButton(index);
                var card = upgradeCards[index];
                var selectedUpgrade = possibleUpgrades[numbers[index]];
                
                SetButton(card, selectedUpgrade);
            }
        }
    }

    private void ShowButton(int index)
    {
        upgradeCards[index].gameObject.SetActive(true);
        foreach (var ui in uiToDisable)
        {
            ui.SetActive(false);
        }
    }

    private void HideButtons()
    {
        foreach (var card in upgradeCards)
        {
            HideButton(card.gameObject);
        }
    }

    private void HideButton(GameObject button)
    {
        button.SetActive(false);
        foreach (var ui in uiToDisable)
        {
            ui.SetActive(true);
        }
    }

    private void SetButton(Button button, UpgradeData selectedUpgrade)
    {
        var text = button.GetComponentInChildren<TextMeshProUGUI>();
        text.text = selectedUpgrade.upgradeText;
        
        button.onClick.AddListener(delegate
        {
            selectedUpgrade.ApplyUpgrade(gameObject);
            button.onClick.RemoveAllListeners();
            HideButtons();
            TimeManager.ResumeTime();
            upgradeCamera.SetActive(false);
            appliedUpgrades.Add(selectedUpgrade);
        });
    }


    private static int[] GenerateDistinctRandomNumbers(int count, int min, int max)
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
}