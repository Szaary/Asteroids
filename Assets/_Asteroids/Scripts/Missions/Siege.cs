using UnityEngine;

[CreateAssetMenu(fileName = "Menu_Siege", menuName = "Menu/Siege")]
public class Siege : Mission
{
    public override bool CanShow(GameObject target)
    {
        return true;
    }

    public override void Apply(GameObject target)
    {
        GameManager.ChangeGameState(GameState.Mission);
        target.SetActive(true);
    }
}