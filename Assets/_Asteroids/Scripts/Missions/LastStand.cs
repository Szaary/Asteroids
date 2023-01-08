using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu_LastStand", menuName = "Menu/LastStand")]
public class LastStand : Mission
{
    [SerializeField] private float missionTimeInSeconds;
    private float missionTimer;
    
    public override bool CanShow(GameObject target)
    {
        return true;
    }

    public override void Apply(GameObject target)
    {
        GameManager.ChangeGameState(GameState.Mission);

        CreateTimeObjective(EndMission);

        missionTimer = missionTimeInSeconds;

        GameManager.Instance.CreateTimer(() =>
        {
            missionTimer -= Time.deltaTime;
            GameManager.TimerTicked.Invoke(missionTimer);
            EndMission(missionTimer);
        });
        GameManager.GameStateChanged += OnGameStateChanged;
    }
    
    private void OnGameStateChanged(GameState state)
    {
        if (state != GameState.Mission)
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }
    }
    
    private void EndMission(float timer)
    {
        if (missionTimer < 0.0f)
        {
            GameManager.Instance.RemoveTimer();
            OnEndMission();
        }
    }
}