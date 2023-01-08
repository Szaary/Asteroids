using UnityEngine;

[CreateAssetMenu(fileName = "MenuContainer_", menuName = "Menu/MenuContainer")]
public class MenuContainer : MenuData
{
    [SerializeField] protected Container container;
    [SerializeField] private string menuName;
    public override (Resource, int) GetCost()
    {
        return (resource, unlockCost);
    }

    public override bool CanShow(GameObject target)
    {
        return true;
    }

    public override void Apply(GameObject target)
    {
        UiCards.GenerateCards(menuName, container.GetUpgrades(), target);
    }
}