using UnityEngine;

[CreateAssetMenu(fileName = "Menu_Defeat", menuName = "Menu/Defeat")]
public class Defeat : MenuData
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