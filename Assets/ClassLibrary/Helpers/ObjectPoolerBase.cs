using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolerBase : MonoBehaviour
{
    [SerializeField] protected List<GameObject> objectToPool = new List<GameObject>();

    [SerializeField] protected int amountToPool;
    [SerializeField] protected List<GameObject> pooledObjects = new List<GameObject>();

    protected virtual void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj;
            if (objectToPool.Count>1)
            {
                int randomNumber = Random.Range(0, objectToPool.Count);
                obj = Instantiate(objectToPool[randomNumber]);
            }
            else
            {
                obj = Instantiate(objectToPool[0]);
            }
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }

    protected virtual GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    protected virtual GameObject GetRandomPooledObject()
    {
        while (pooledObjects.Count <= amountToPool)
        {

            int i = Random.Range(0, amountToPool);
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }


    public virtual void SpawnObjectFromPool(bool isRandom, GameObject spawner, Vector3 spawnPosition)
    {
        GameObject pooledObject;
        if (isRandom)
        {
            pooledObject = GetRandomPooledObject();
        }
        else
        {
            pooledObject = GetPooledObject();
        }

        if (pooledObject != null)
        {
            pooledObject.SetActive(true);
            pooledObject.transform.position = spawner.transform.position + spawnPosition;
        }
    }
    public virtual void DespawnObject(GameObject spawnedObject)
    {
        spawnedObject.SetActive(false);
    }
    public virtual void DespawnAllObjects()
    {
        foreach (GameObject pooledObject in pooledObjects)
        {
            pooledObject.SetActive(false);
        }
    }

}
