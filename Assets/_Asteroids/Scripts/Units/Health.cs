using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;

    public bool IsAlive => currentHealth > 0;
    public virtual void Kill() => Damage(maxHealth);
    
    protected virtual void OnEnable() => currentHealth = maxHealth;

    public virtual void Damage(float damage)
    {
        if (!IsAlive) return;
        
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    protected abstract void OnDeath();
}