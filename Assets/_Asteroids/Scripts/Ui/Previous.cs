
using UnityEngine;

[CreateAssetMenu(fileName = "Menu_Previous", menuName = "Menu/Previous")]
public class Previous : MenuData
{
    public override (Resource, int) GetCost()
    {
        return (resource, unlockCost);
    }

    public override bool CanShow(GameObject target)
    {
        return GameManager.State != GameState.Mission;
    }
    
    public override void Apply(GameObject target)
    {
        SystemsFacade.Instance.uiCards.AddPrevious();
    }
}