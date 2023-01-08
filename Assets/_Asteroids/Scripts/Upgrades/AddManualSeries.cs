using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_AddManualSeries", menuName = "UpgradeData/AddManualSeries")]
public class AddManualSeries : Upgrade
{
    protected override bool OnCanShow(GameObject target)
    {
        return true;
    }

    protected override void OnApply(GameObject target)
    {
        var spawner = target.GetComponentInChildren<MissileSpawner>();
        spawner.enabled = true;
        spawner.numberOfSeries++;
    }
}