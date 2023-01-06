using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : Health
{
    [SerializeField] private int experienceGain = 5;
    [SerializeField] private float damageToPlayerOnHit = 10;

    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private AudioClip explodeSound;

    public WaveManager manager { get; set; }

    private LevelSystem _levelSystem;
    private Rigidbody _enemyRb;
    private GameObject _player;
    private AudioSource _audioSource;


    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
        _levelSystem = _player.GetComponent<LevelSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            Debug.Log("AutoDestruction after hit player!");
            health.Damage(damageToPlayerOnHit);
            Kill();
        }
    }

    public void Destroy()
    {
        manager.RegisterDestroy(this);
    }

    protected override void OnDeath()
    {
        _levelSystem.AddExperience(experienceGain);
        _audioSource.PlayOneShot(explodeSound);
        Instantiate(destroyParticle, transform);
        LoseControlOfShip();
        manager.RegisterKill(this);
    }

    private void LoseControlOfShip()
    {
        float x = Random.Range(-2, 2);
        float y = Random.Range(-15, 15);
        float z = Random.Range(10, 20);
        _enemyRb.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
    }
}