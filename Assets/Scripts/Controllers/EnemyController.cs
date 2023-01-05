using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int experienceGain;
    
    [SerializeField] private float _trust;
    [SerializeField] private float _boostTrust;
    [SerializeField] private float _stopRange;
    [SerializeField] private float _floatingTime;

    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private AudioClip _explodeSound;
    [SerializeField] private AudioClip _startBoost;
   
    private bool _isAlive;
    private AudioSource _audioSource;
    private bool _canPlaySound = true;
    private LevelSystem _levelSystem;
    private Rigidbody _enemyRb;
    private GameObject _player;
    private bool _canMove;

    public EnemySpawnManager manager { get; set; }

    private void Awake()
    {
        _player = GameObject.Find("Player");
        _enemyRb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _levelSystem = _player.GetComponent<LevelSystem>();
    }

    private void OnEnable()
    {
        _enemyRb.AddForce(Vector3.back * _trust, ForceMode.Impulse);
        _isAlive = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _enemyRb.velocity= Vector3.zero;
        _enemyRb.angularVelocity = Vector3.zero;
        _canMove = false;
        _boostTrust = 0;
    }

    private void Update()
    {
        StopEnemyAfterSpawn();
        
        if (transform.position.z < -5)
        {
            StartCoroutine(DestroyMyselfAfterSomeTime(0.1f));
        }
    }

    private void StopEnemyAfterSpawn()
    {
        var range = Random.Range(0, _stopRange);
        if ((transform.position.z + range) < GameManager.Instance.borderZ && _canMove == false)
        {
            _enemyRb.velocity *= 0.015f;
            _canMove = true;
            LookAtPlayer();
        }
        if (_canMove)
        {
            _boostTrust = 0.75f;
            StartCoroutine(SendEnemyToPlayer());
        }
    }

    private void LookAtPlayer()
    {
        var lookVector = _player.transform.position - transform.position;
        var rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }


    private IEnumerator SendEnemyToPlayer()
    {
        if (_isAlive)
        {
            yield return new WaitForSeconds(GameManager.Instance.enemyStopTime);
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
        var lookDirection = (_player.transform.position - transform.position).normalized;
        var x = Random.Range(-0.5f, 0.5f);
        lookDirection.x += x;
        return lookDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {   
            StartCoroutine(DestroyMyselfAfterSomeTime(_floatingTime));
        }
        
        if (collision.collider.CompareTag("Missile"))
        {
            _audioSource.PlayOneShot(_explodeSound);
            ChangePositionAfterHitByMissile();
            Instantiate(destroyParticle, transform);
            collision.gameObject.SetActive(false);
            if (_isAlive)
            {
                GameManager.Instance.enemiesKilled++;
                EventBroker.CallExplodeAction();
                StartCoroutine(DestroyMyselfAfterSomeTime(_floatingTime));
            }
            _isAlive = false;
            _levelSystem.AddExperience(experienceGain);
        }
    }

    private void ChangePositionAfterHitByMissile()
    {
        float x = Random.Range(-2, 2);
        float y = Random.Range(-15, 15);
        float z = Random.Range(10, 20);
        _enemyRb.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
    }

    private IEnumerator DestroyMyselfAfterSomeTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        manager.Remove(this);
    }
}
