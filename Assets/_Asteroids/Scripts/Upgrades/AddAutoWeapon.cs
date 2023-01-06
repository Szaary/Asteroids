using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_AddAutoWeapon", menuName = "UpgradeData/AddAutoWeapon")]
public class AddAutoWeapon : UpgradeData
{
    public override bool CanShow(GameObject player)
    {
        return true;
    }

    public override void ApplyUpgrade(GameObject player)
    {
        var autoFire = player.GetComponentInChildren<AutoFire>();
        autoFire.enabled = true;
        autoFire.numberOfWeapons++;
    }
}