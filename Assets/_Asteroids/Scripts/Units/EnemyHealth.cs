using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : Health
{
    public int experienceGain = 5;
    [SerializeField] private float damageToPlayerOnHit = 10;

    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private AudioClip explodeSound;

    public WaveManager waveManager { get; set; }
    private Rigidbody _enemyRb;
    private AudioSource _audioSource;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody>();

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            health.Damage(damageToPlayerOnHit);
            Kill();
        }
    }

    public void Destroy()
    {
        waveManager.RegisterDestroy(this);
    }

    protected override void OnDeath()
    {
        _audioSource.PlayOneShot(explodeSound);
        Instantiate(destroyParticle, transform);
        LoseControlOfShip();
        waveManager.RegisterKill(this);
    }

    private void LoseControlOfShip()
    {
        float x = Random.Range(-2, 2);
        float y = Random.Range(-15, 15);
        float z = Random.Range(10, 20);
        _enemyRb.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
    }
}