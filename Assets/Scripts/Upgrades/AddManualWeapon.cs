using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_AddManualWeapon", menuName = "UpgradeData/AddManualWeapon")]
public class AddManualWeapon : UpgradeData
{
    public override bool CanShow(GameObject player)
    {
        return true;
    }

    public override void ApplyUpgrade(GameObject player)
    {
        var spawner = player.GetComponentInChildren<MissileSpawner>();
        spawner.enabled = true;
        spawner.numberOfWeapons++;
    }
}