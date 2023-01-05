using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;

    private void Start()
    {
        pooledObjects = new List<GameObject>();
    }

    public GameObject SpawnObjectFromPool()
    {
        var pooledObject = GetPooledObject();
        if (pooledObject != null)
        {
            pooledObject.SetActive(true);
        }
        return pooledObject;
    }

    private GameObject GetPooledObject()
    {
        foreach (var pooled in pooledObjects)
        {
            if (!pooled.activeInHierarchy)
            {
                return pooled;
            }
        }
        var obj = Instantiate(objectToPool);
        pooledObjects.Add(obj);
        return obj;
    }
}
