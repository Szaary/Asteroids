using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Transform objectivesParent;
    [SerializeField] private Objective objectivePrefab;

    [Header("HUD references")] [SerializeField]
    private List<Objective> objectives;

    [SerializeField] private Image joystick;
    [SerializeField] private Image fire;

    [SerializeField] private Image autoFireBorder;
    [SerializeField] private Image autoFireCooldown;

    [SerializeField] private Image healthBar;

    [SerializeField] private Image experienceBar;
    [SerializeField] private TextMeshProUGUI level;


    private void Awake()
    {
        GameManager.GameStateChanged += OnGameStateChanged;

        PlayerHealth.HealthChanged += OnHealthChanged;
        LevelSystem.ExperienceChanged += OnExperienceChanged;

        UiCards.CardsEnabled += OnCardsEnabled;

        AutoFire.CounterChanged += OnAutoFireChanged;
        AutoFire.StatusChanged += OnAutoFireStatusChanged;
    }


    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState is GameState.Mission)
        {
            SetupBasicMission();
        }
        else
        {
            HideHud();
            RemoveObjectives();
        }
    }

    public Objective CreateObjective()
    {
        var objective = Instantiate(objectivePrefab, objectivesParent);
        objectives.Add(objective);
        return objective;
    }


    private void RemoveObjectives()
    {
        for (var index = objectives.Count - 1; index >= 0; index--)
        {
            var objective = objectives[index];
            Destroy(objective.gameObject);
        }

        objectives.Clear();
    }

    private void SetupBasicMission()
    {
        experienceBar.fillAmount = 0;
        healthBar.fillAmount = 1;
        autoFireCooldown.fillAmount = 0f;
        ShowHud();
    }

    private void OnAutoFireStatusChanged(bool isEnabled)
    {
        autoFireBorder.enabled = isEnabled;
        autoFireCooldown.enabled = isEnabled;
    }

    private void OnAutoFireChanged(float autoFire)
    {
        autoFireCooldown.fillAmount = autoFire;
    }

    private void OnCardsEnabled(bool cardsEnabled)
    {
        if (cardsEnabled)
            HideHud();
        else
            ShowHud();
    }

    
    private void ShowHud()
    {
        foreach (var objective in objectives)
        {
            objective.SetActive(true);
        }

        joystick.gameObject.SetActive(true);
        fire.gameObject.SetActive(true);

        healthBar.gameObject.SetActive(true);

        experienceBar.gameObject.SetActive(true);
        level.gameObject.SetActive(true);
        autoFireBorder.gameObject.SetActive(true);
    }

    
    private void HideHud()
    {
        foreach (var objective in objectives)
        {
            objective.SetActive(false);
        }

        joystick.gameObject.SetActive(false);
        fire.gameObject.SetActive(false);

        healthBar.gameObject.SetActive(false);

        experienceBar.gameObject.SetActive(false);
        level.gameObject.SetActive(false);
        autoFireBorder.gameObject.SetActive(false);
    }

  

    private void OnExperienceChanged(float before, float after, int currentLevel, bool leveled)
    {
        if (leveled)
        {
            DOTween.Sequence()
                .Append(DOVirtual.Float(before, 1, 0.2f, SetBar).SetEase(Ease.OutExpo).SetUpdate(true))
                .Append(DOVirtual.Float(0, after, 0.2f, SetBar)
                    .SetEase(Ease.OutExpo).SetUpdate(true));
            level.text = $"Level: {currentLevel}";
        }
        else
        {
            DOVirtual.Float(before, after, 0.4f, SetBar).SetEase(Ease.OutExpo)
                .SetUpdate(true);
        }

        void SetBar(float percentage) => experienceBar.fillAmount = percentage;
    }

    private void OnHealthChanged(float before, float after, float damage, bool isAlive)
    {
        DOVirtual.Float(before, after, 0.4f, SetBar).SetEase(Ease.OutExpo)
            .SetUpdate(true);
        void SetBar(float percentage) => healthBar.fillAmount = percentage;
    }


    private void OnDestroy()
    {
        PlayerHealth.HealthChanged -= OnHealthChanged;
        LevelSystem.ExperienceChanged -= OnExperienceChanged;
        UiCards.CardsEnabled -= OnCardsEnabled;
        AutoFire.CounterChanged -= OnAutoFireChanged;
        AutoFire.StatusChanged -= OnAutoFireStatusChanged;
        GameManager.GameStateChanged -= OnGameStateChanged;
    }
}