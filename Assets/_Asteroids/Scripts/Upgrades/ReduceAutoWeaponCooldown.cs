using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_ReduceAutoWeaponCooldown", menuName = "UpgradeData/ReduceAutoWeaponCooldown")]
public class ReduceAutoWeaponCooldown : Upgrade
{
    public Upgrade requiredUpgrade;

    protected override bool OnCanShow(GameObject target)
    {
        var manager = target.GetComponent<UpgradeManager>();
        var contains = manager.appliedUpgrades.Contains(requiredUpgrade);

        var autoFire = target.GetComponentInChildren<AutoFire>();
        var hasNotReachedLimit = autoFire.cooldownTime > 0.4f;
        return contains && hasNotReachedLimit;
    }

    protected override void OnApply(GameObject target)
    {
        var autoFire = target.GetComponentInChildren<AutoFire>();
        autoFire.enabled = true;
        autoFire.cooldownTime -= 0.3f;
    }
}