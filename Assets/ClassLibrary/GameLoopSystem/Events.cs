using UnityEngine.Events;

public class Events
{
    [System.Serializable]
    public class EventFadeComplete
        : UnityEvent<bool>
    { }

    [System.Serializable]
    public class EventGameState
        : UnityEvent<GameManagerBase.GameState, GameManagerBase.GameState>
    { }


}
