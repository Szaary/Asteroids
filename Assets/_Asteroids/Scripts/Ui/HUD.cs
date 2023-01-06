using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("HUD references")] [SerializeField]
    private TextMeshProUGUI enemiesKilled;

    [SerializeField] private TextMeshProUGUI wave;

    [SerializeField] private Image joystick;
    [SerializeField] private Image fire;

    [SerializeField] private Image autoFireBorder;
    [SerializeField] private Image autoFireCooldown;

    [SerializeField] private Image healthBar;

    [SerializeField] private Image experienceBar;
    [SerializeField] private TextMeshProUGUI level;


    private void Awake()
    {
        experienceBar.fillAmount = 0;
        healthBar.fillAmount = 1;
        autoFireCooldown.fillAmount = 0f;

        WaveManager.enemyKilled += OnEnemyKilled;
        WaveManager.waveSpawned += OnWaveChanged;

        PlayerHealth.HealthChanged += OnHealthChanged;
        LevelSystem.ExperienceChanged += OnExperienceChanged;

        UiCards.CardsEnabled += OnCardsEnabled;

        AutoFire.CounterChanged += OnAutoFireChanged;
        AutoFire.StatusChanged += OnAutoFireStatusChanged;
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

    private void HideHud()
    {
        enemiesKilled.gameObject.SetActive(false);
        wave.gameObject.SetActive(false);

        joystick.gameObject.SetActive(false);
        fire.gameObject.SetActive(false);

        healthBar.gameObject.SetActive(false);

        experienceBar.gameObject.SetActive(false);
        level.gameObject.SetActive(false);
        autoFireBorder.gameObject.SetActive(false);
    }

    private void ShowHud()
    {
        enemiesKilled.gameObject.SetActive(true);
        wave.gameObject.SetActive(true);

        joystick.gameObject.SetActive(true);
        fire.gameObject.SetActive(true);

        healthBar.gameObject.SetActive(true);

        experienceBar.gameObject.SetActive(true);
        level.gameObject.SetActive(true);
        autoFireBorder.gameObject.SetActive(true);
    }

    private void OnWaveChanged(int value) => wave.text = "Wave: " + value;
    private void OnEnemyKilled(int value) => enemiesKilled.text = "Enemies Killed: " + value;

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
        WaveManager.enemyKilled -= OnEnemyKilled;
        WaveManager.waveSpawned -= OnWaveChanged;
        PlayerHealth.HealthChanged -= OnHealthChanged;
        LevelSystem.ExperienceChanged -= OnExperienceChanged;
        UiCards.CardsEnabled -= OnCardsEnabled;
        AutoFire.CounterChanged -= OnAutoFireChanged;
        AutoFire.StatusChanged -= OnAutoFireStatusChanged;
    }
}