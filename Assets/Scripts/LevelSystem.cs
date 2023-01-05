using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image experienceBar;

    private int level;
    private int experience;
    private int experienceToNextLevel;


    private void Awake()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 20;
        experienceBar.fillAmount = 0;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        var tempExp = (float) experience / experienceToNextLevel;
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;

            levelText.text = "Level: " + level;
            experienceToNextLevel += experienceToNextLevel / 2;
            upgradeManager.GenerateUpgrades();

            DOTween.Sequence()
                .Append(DOVirtual.Float(tempExp, 1, 0.2f, SetBar).SetEase(Ease.OutExpo).SetUpdate(true))
                .Append(DOVirtual.Float(0, (float) experience / experienceToNextLevel, 0.2f, SetBar)
                    .SetEase(Ease.OutExpo).SetUpdate(true));
        }
        else
        {
            DOVirtual.Float(tempExp, (float) experience / experienceToNextLevel, 0.4f, SetBar).SetEase(Ease.OutExpo)
                .SetUpdate(true);
        }
    }

    private void SetBar(float percentage)
    {
        experienceBar.fillAmount = percentage;
    }
}