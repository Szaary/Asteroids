using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_AddManualWeapon", menuName = "UpgradeData/AddManualWeapon")]
public class AddManualWeapon : Upgrade
{
    protected override bool OnCanShow(GameObject target)
    {
        return true;
    }

    protected override void OnApply(GameObject target)
    {
        var spawner = target.GetComponentInChildren<MissileSpawner>();
        spawner.enabled = true;
        spawner.numberOfWeapons++;
    }
}