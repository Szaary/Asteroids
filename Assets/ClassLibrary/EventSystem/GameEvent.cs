using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event", fileName ="New Game Event")]
public class GameEvent : ScriptableObject
{
    HashSet<GameEventListener> _eventlisteners = new HashSet<GameEventListener>();

    public void Invoke()
    {
        foreach(var eventListener in _eventlisteners)
        {
            eventListener.RaiseEvent();
        }
    }
    public void Register(GameEventListener gameEventListener)
    {
        _eventlisteners.Add(gameEventListener);
    }
    public void Deregister(GameEventListener gameEventListener)
    {
        _eventlisteners.Remove(gameEventListener);
    }

    
}
