using UnityEngine;

public abstract class Upgrade : MenuData
{
    [SerializeField] private bool canBePermanentlyPurchased;

    public override (Resource, int) GetCost()
    {
        return (resource, unlockCost);
    }

    public override bool CanShow(GameObject target)
    {
        if (GameManager.State == GameState.Mission)
        {
            return OnCanShow(target);
        }

        var upgradeManager = target.GetComponentInChildren<UpgradeManager>();
        return canBePermanentlyPurchased
               && OnCanShow(target)
               && !upgradeManager.purchasedUpgrades.Contains(this);
    }

    public override void Apply(GameObject target)
    {
        if (GameManager.State == GameState.Mission)
        {
            OnApply(target);
        }
        else
        {
            var upgradeManager = target.GetComponentInChildren<UpgradeManager>();
            upgradeManager.purchasedUpgrades.Add(this);
        }
    }

    protected abstract void OnApply(GameObject target);
    protected abstract bool OnCanShow(GameObject target);
}