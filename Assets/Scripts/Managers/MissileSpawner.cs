using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private AudioClip shot;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button manualShotButton;
    public int numberOfWeapons = 1;
    public int numberOfSeries = 1;
    
    private void Awake()
    {
        manualShotButton.onClick.AddListener(ManualShoot);
    }

    private void OnDestroy()
    {
        manualShotButton.onClick.RemoveListener(ManualShoot);
    }


    private void ManualShoot()
    {
        PlayerMovementController.BackOnShot(playerRb, 5f);
        manualShotButton.transform.DOShakeScale(0.2f, 0.1f, 1);
        StartCoroutine(Shot(numberOfSeries, numberOfWeapons));
    }

    public void AutomaticShoot(int series, int weapons)
    {
        StartCoroutine(Shot(series, weapons));
    }

    private IEnumerator Shot(int series, int weapons)
    {
        audioSource.PlayOneShot(shot);
        
        for (var i = 0; i <= series; i++)
        {
            for (var j = 0; j < weapons; j++)
            {
                var spawned = pool.SpawnObjectFromPool();
                spawned.transform.position = CalculateSpawnPosition();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private Vector3 CalculateSpawnPosition()
    {
        var position = Random.Range(-0.5f, 0.5f);
        var output = transform.position + new Vector3(position, 0, 0);

        return output;
    }
    
}