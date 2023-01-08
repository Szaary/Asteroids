using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_AddAutoWeapon", menuName = "UpgradeData/AddAutoWeapon")]
public class AddAutoWeapon : Upgrade
{
    protected override bool OnCanShow(GameObject target)
    {
        return target.GetComponentInChildren<AutoFire>().enabled;
    }

    protected override void OnApply(GameObject target)
    {
        var autoFire = target.GetComponentInChildren<AutoFire>();
        autoFire.numberOfWeapons++;
    }
}