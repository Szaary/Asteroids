using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public enum State
    {
        Spawned,
        InPosition,
        Chasing,
        Dead
    }

    [SerializeField] private State state;
    [SerializeField] private float _trust;
    [SerializeField] private float boostTrust;

    [SerializeField] private float _stopRange;
    [SerializeField] private float enemyStopTime = 2;

    [SerializeField] private AudioClip _startBoost;
    [SerializeField] private EnemyHealth health;

    private AudioSource _audioSource;

    private Rigidbody _enemyRb;
    private GameObject _player;
    public WaveManager waveManager { get; set; }

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _player = waveManager.Player;
    }

    private void OnEnable()
    {
        _enemyRb.AddForce(Vector3.back * _trust, ForceMode.Impulse);
        state = State.Spawned;
    }

    private void Update()
    {
        if (!health.IsAlive) state = State.Dead;

        switch (state)
        {
            case State.Spawned:
            {
                var range = Random.Range(0, _stopRange);
                if (transform.position.z + range < GameManager.Instance.borderZ)
                {
                    _enemyRb.velocity *= 0.01f;
                    StartCoroutine(SetTimerOnChase());
                    state = State.InPosition;
                }
                break;
            }
            case State.InPosition:
                var lookVector = _player.transform.position - transform.position;
                var rot = Quaternion.LookRotation(lookVector);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
                break;
            case State.Chasing:
                _enemyRb.AddForce(GenerateEnemyTargetLocation() * boostTrust, ForceMode.Impulse);
                if (transform.position.z < -10 || Mathf.Abs(transform.position.x) > 400)
                {
                    state = State.Dead;
                    health.Destroy();
                }
                break;
            case State.Dead:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private IEnumerator SetTimerOnChase()
    {
        yield return new WaitForSeconds(enemyStopTime);
        _audioSource.PlayOneShot(_startBoost);
        state = State.Chasing;
    }
    
    private Vector3 GenerateEnemyTargetLocation()
    {
        var lookDirection = (_player.transform.position - transform.position).normalized;
        lookDirection.x += Random.Range(-0.5f, 0.5f);
        return lookDirection;
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
        _enemyRb.velocity = Vector3.zero;
        _enemyRb.angularVelocity = Vector3.zero;
    }
}