using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInvoker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameEvent _gameEvent;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        _gameEvent?.Invoke();
    }
}
