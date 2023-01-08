using System;
using UnityEngine;

public enum GameState
{
    Startup,
    Menu,
    Mission,
    Victory,
    Defeat
}

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> GameStateChanged;
    public static Action<float> TimerTicked;

    public static GameState State { get; private set; }

    public static void ChangeGameState(GameState state)
    {
        if (state == State) return;
        State = state;

        if (state == GameState.Mission)
        {
            SystemsFacade.Instance.player.SetActive(true);
        }

        GameStateChanged?.Invoke(State);
    }


    public float borderX { get; set; }
    public float borderY { get; set; }
    public float borderZ { get; set; }

    private Action _missionTimer;

    protected override void Awake()
    {
        base.Awake();

        borderX = 40;
        borderY = 40;
        borderZ = 120;
    }

    private void Start()
    {
        ChangeGameState(GameState.Menu);
    }

    public void CreateTimer(Action timer) => _missionTimer = timer;
    public void RemoveTimer() => _missionTimer = null;
    private void Update() => _missionTimer?.Invoke();
}