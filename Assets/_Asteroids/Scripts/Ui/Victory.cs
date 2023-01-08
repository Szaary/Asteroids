using UnityEngine;

[CreateAssetMenu(fileName = "Menu_Victory", menuName = "Menu/Victory")]
public class Victory : MenuData
{
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
        GameManager.ChangeGameState(GameState.Menu);
        target.SetActive(false);
    }
}