using UnityEngine;

public abstract class UpgradeData : ScriptableObject
{
    public string upgradeText;
    
    public abstract bool CanShow(GameObject player);
    public abstract void ApplyUpgrade(GameObject player);
}