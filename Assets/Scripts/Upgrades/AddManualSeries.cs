using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_AddManualSeries", menuName = "UpgradeData/AddManualSeries")]
public class AddManualSeries : UpgradeData
{
    public override bool CanShow(GameObject player)
    {
        return true;
    }

    public override void ApplyUpgrade(GameObject player)
    {
        var spawner = player.GetComponentInChildren<MissileSpawner>();
        spawner.enabled = true;
        spawner.numberOfSeries++;
    }
}