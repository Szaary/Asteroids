using UnityEngine;
using UnityEngine.UI;

public class AutoFire : MonoBehaviour
{
    public float cooldownTime = 5f;
    
    public int numberOfWeapons;
    public int numberOfSeries = 1;
    
    [SerializeField] private Image cooldownImage;
    [SerializeField] private Image cooldownBorder;

    [SerializeField] private MissileSpawner spawner;

    private float cooldownTimer;

    private void Start()
    {
        cooldownImage.fillAmount = 0f;
    }

    private void OnEnable()
    {
        cooldownImage.enabled = true;
        cooldownBorder.enabled = true;
    }

    private void OnDisable()
    {
        cooldownImage.enabled = false;
        cooldownBorder.enabled = false;
    }

    private void Update()
    {
        ApplyCooldown();
    }

    private void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0.0f)
        {
            cooldownImage.fillAmount = 0f;
            spawner.AutomaticShoot(numberOfSeries, numberOfWeapons);
            cooldownTimer = cooldownTime;
        }
        else
        {
            cooldownImage.fillAmount = cooldownTimer / cooldownTime;
        }
    }
}