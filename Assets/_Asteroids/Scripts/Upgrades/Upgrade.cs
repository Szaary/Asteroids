using UnityEngine;

public abstract class Upgrade : MenuData
{
    [SerializeField] private bool canBePermanentlyPurchased;

    public override (Resource, int) GetCost()
    {
        return GameManager.State == GameState.Menu ? (resource, unlockCost) : (resource, 0);
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
               && !upgradeManager.Unlocked(this);
    }

    public override bool CanApply()
    {
        return GameManager.State != GameState.Menu || ResourcesManager.Instance.TryBuy(resource, unlockCost);
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
            upgradeManager.Unlock(this);
        }
    }

    protected abstract void OnApply(GameObject target);
    protected abstract bool OnCanShow(GameObject target);
}