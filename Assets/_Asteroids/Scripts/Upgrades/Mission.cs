using System;
using UnityEngine;

public abstract class Mission : MenuData
{
    [SerializeField] protected string uiDescription;
    [SerializeField] protected int reward;

    public override (Resource, int) GetCost()
    {
        // TODO Create unlocking
        // if is unlocked return reward
        // return unlockCost;
        return (resource, reward);
    }

    protected void CreateTimeObjective(Action<float> onTick)
    {
        var objective = SystemsFacade.Instance.hud.CreateObjective();
        Action<string> set = objective.Set;
        GameManager.TimerTicked += UpdateTimer;
        objective.SetDestroy(() => { GameManager.TimerTicked -= UpdateTimer; });
        
        void UpdateTimer(float time)
        {
            var minutes = Mathf.FloorToInt(time / 60);
            var seconds = Mathf.FloorToInt(time % 60);
            var text = $"{uiDescription}: {minutes:00}:{seconds:00}";
            set?.Invoke(text);
            
            onTick?.Invoke(time);
        }
    }
    
    protected void CreateKillObjective(Action<int, int> onCounter)
    {
        var objective = SystemsFacade.Instance.hud.CreateObjective();
        Action<string> set = objective.Set;
        WaveManager.EnemyKilled += UpdateCounter;
        var info = SystemsFacade.Instance.waveManager.GetEnemies;
        UpdateCounter(info.Item1, info.Item2);
        objective.SetDestroy(() => { WaveManager.EnemyKilled -= UpdateCounter; });
        
        void UpdateCounter(int value, int endValue = 0)
        {
            var text = endValue == 0 ? $"{uiDescription}: {value}" : $"{uiDescription}: {value} / {endValue}";
            set(text);
            onCounter?.Invoke(value, endValue);
        }
    }

    protected void OnEndMission()
    {
        if (GameManager.State != GameState.Mission) return;
        GameManager.ChangeGameState(GameState.Victory);
        SystemsFacade.Instance.resourceManager.Increase(resource, reward);
    }
}