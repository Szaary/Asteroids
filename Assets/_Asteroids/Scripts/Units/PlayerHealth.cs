using System;

public class PlayerHealth : Health
{
    public static event Action<float, float, float, bool> HealthChanged;

    public override void Damage(float damage)
    {
        var before = currentHealth / maxHealth;
        base.Damage(damage);
        var after = currentHealth / maxHealth;

        HealthChanged?.Invoke(before, after, damage, IsAlive);
    }

    protected override void OnDeath()
    {
        GameManager.ChangeGameState(GameState.Defeat);
    }
}