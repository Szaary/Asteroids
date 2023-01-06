using System;
using UnityEngine;

public class AutoFire : MonoBehaviour
{
    public static event Action<float> CounterChanged;
    public static event Action<bool> StatusChanged;

    public float cooldownTime = 5f;

    public int numberOfWeapons;
    public int numberOfSeries = 1;

    [SerializeField] private MissileSpawner spawner;

    private float cooldownTimer;

    private void OnEnable()
    {
        StatusChanged?.Invoke(true);
    }

    private void OnDisable()
    {
        StatusChanged?.Invoke(false);
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
            spawner.AutomaticShoot(numberOfSeries, numberOfWeapons);
            cooldownTimer = cooldownTime;
        }

        CounterChanged?.Invoke(cooldownTimer / cooldownTime);
    }
}