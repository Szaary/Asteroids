using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_EnableAutoWeapon", menuName = "UpgradeData/EnableAutoWeapon")]
public class EnableAutoWeapon : Upgrade
{
    protected override bool OnCanShow(GameObject target)
    {
        return !target.GetComponentInChildren<AutoFire>().enabled;
    }

    protected override void OnApply(GameObject target)
    {
        var autoFire = target.GetComponentInChildren<AutoFire>();
        autoFire.numberOfWeapons++;
        autoFire.enabled = true;
    }
}