using UnityEngine;

[CreateAssetMenu(fileName = "Menu_Annihilation", menuName = "Menu/Annihilation")]
public class Annihilation : Mission
{
    [SerializeField] private int enemiesToKill;

    public override bool CanShow(GameObject target)
    {
        return true;
    }

    public override void Apply(GameObject target)
    {
        GameManager.ChangeGameState(GameState.Mission);
        SystemsFacade.Instance.waveManager.enemiesToKill = enemiesToKill;
        CreateKillObjective(EndMission);
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state != GameState.Mission)
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }
    }

    private void EndMission(int value, int endValue = 0)
    {
        if (value == endValue)
        {
            OnEndMission();
        }
    }
}