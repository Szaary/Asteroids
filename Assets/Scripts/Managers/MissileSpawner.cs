using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{    
    [SerializeField] private GameObject _missile;


    private void Start()
    {
        EventBroker.ShotAction += StartIShotCorutine;
    }
    private void OnDisable()
    {
        EventBroker.ShotAction -= StartIShotCorutine;
    }

    public void SendMissile()
    {
        for (int i = 0; i < GameManagerAsteroids.Instance.numberOfWeapons; i++)
        {
            Instantiate(_missile, CalculateSpawnPosition(), _missile.transform.rotation);
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        float position =Random.Range(-0.5f, 0.5f);
        Vector3 output = transform.position+new Vector3(position, 0,0);

        return output;
    }

    private void StartIShotCorutine()
    {
        StartCoroutine(IShot());
    }

    IEnumerator IShot()
    {
        for (int i = 0; i <= GameManagerAsteroids.Instance.numberOfUpgrades; i++)
        {
            SendMissile();

            yield return new WaitForSeconds(0.1f);
        }
    }


}
