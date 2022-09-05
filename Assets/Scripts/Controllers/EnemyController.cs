using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody _enemyRb;
    private GameObject _player;
    private bool _canMove = false;

    [SerializeField] private float _trust;
    [SerializeField] private float _boostTrust;
    [SerializeField] private float _stopRange;
    [SerializeField] private float _floatingTime;
    private bool _isAlive = true;

    private Light _iExplodeLight;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _explodeSound;
    [SerializeField] private AudioClip _startBoost;
    private bool _canPlaySound = true;

    void Start()
    {
        _player = GameObject.Find("Player");
        _enemyRb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _enemyRb.AddForce(Vector3.back * _trust, ForceMode.Impulse);        
        _iExplodeLight = gameObject.GetComponentInChildren<Light>();
    }

    void Update()
    {
        StopEnemyAfterSpawn();
    }

    private void StopEnemyAfterSpawn()
    {
        float range = Random.Range(0, _stopRange);
        if ((transform.position.z + range) < GameManagerAsteroids.Instance.borderZ && _canMove == false)
        {
            _enemyRb.velocity *= 0.015f;
            _canMove = true;
            LookAtPlayer();
        }
        if (_canMove == true)
        {
            _boostTrust = 0.75f;
            StartCoroutine(SendEnemyToPlayer());
        }
    }

    private void LookAtPlayer()
    {
        Vector3 lookVector = _player.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }


    IEnumerator SendEnemyToPlayer()
    {
        if (_isAlive)
        {
            yield return new WaitForSeconds(GameManagerAsteroids.Instance.enemyStopTime);
            if (_canPlaySound)
            {
                _audioSource.PlayOneShot(_startBoost);
                _canPlaySound = false;
            }
            _enemyRb.AddForce(GenerateEnemyTargetLocation() * _boostTrust, ForceMode.Impulse);
        }
    }

    private Vector3 GenerateEnemyTargetLocation()
    {
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;
        float x = Random.Range(-0.5f, 0.5f);
        lookDirection.x += x;
        return lookDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {   
            GameManagerAsteroids.Instance.numberOfWeapons /= 2;
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Missile"))
        {
            _audioSource.PlayOneShot(_explodeSound);
            ChangePositionAfterHitByMissile();
            StartCoroutine(IGenerateExplosionEffectOnHitByMissile());
            Destroy(collision.gameObject);
            if (_isAlive)
            {
                GameManagerAsteroids.Instance.enemiesKilled++;
                EventBroker.CallExplodeAction();
                StartCoroutine(IDestroyMyselfAfterSomeTime());
            }
            _isAlive = false;
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            _enemyRb.velocity = Vector3.zero;            
            transform.position = new Vector3(transform.position.x + Random.Range(-1, 3), 0, transform.position.z + Random.Range(-1, 3));
            LookAtPlayer();
        }
    }

    public void ChangePositionAfterHitByMissile()
    {
        float x = Random.Range(-2, 2);
        float y = Random.Range(-15, 15);
        float z = Random.Range(10, 20);
        _enemyRb.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
    }

    IEnumerator IGenerateExplosionEffectOnHitByMissile()
    {
        _iExplodeLight.range = 0;
        _iExplodeLight.intensity = 0;
        for (float i = 0; i < 2; i+=0.1f)
        {
            _iExplodeLight.range += i/2;
            _iExplodeLight.intensity += i*5;
            yield return new WaitForSeconds(0.001f);          
        }
        for (float i = 0; i < 2; i += 0.1f)
        {
            _iExplodeLight.range -= i/2;
            _iExplodeLight.intensity -= i * 5;
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator IDestroyMyselfAfterSomeTime()
    {
        yield return new WaitForSeconds(_floatingTime);
        Destroy(gameObject);
    }
}
