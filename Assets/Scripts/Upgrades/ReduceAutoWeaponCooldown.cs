using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_ReduceAutoWeaponCooldown", menuName = "UpgradeData/ReduceAutoWeaponCooldown")]
public class ReduceAutoWeaponCooldown : UpgradeData
{
    public UpgradeData requiredUpgrade;
    
    public override bool CanShow(GameObject player)
    {
        var manager = player.GetComponent<UpgradeManager>();
        var contains =manager.appliedUpgrades.Contains(requiredUpgrade);
        
        var autoFire = player.GetComponentInChildren<AutoFire>();
        var hasNotReachedLimit = autoFire.cooldownTime > 0.4f; 
        return contains && hasNotReachedLimit;
    }

    public override void ApplyUpgrade(GameObject player)
    {
        var autoFire = player.GetComponentInChildren<AutoFire>();
        autoFire.enabled = true;
        autoFire.cooldownTime -= 0.3f;
    }
}

