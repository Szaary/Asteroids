using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Container_", menuName = "Menu/Container")]
public class Container : ScriptableObject
{
    public List<MenuData> upgrades;

    public List<MenuData> GetUpgrades()
    {
        if (GameManager.State == GameState.Mission) return upgrades;
        var menuData = new List<MenuData>();
        foreach (var data in upgrades)
        {
            if (data is Upgrade upgrade)
            {
                if (upgrade.CanShow(SystemsFacade.Instance.player))
                {
                    menuData.Add(upgrade);
                }
            }
            else
            {
                menuData.Add(data);
            }
        }
        return menuData;
    }
}