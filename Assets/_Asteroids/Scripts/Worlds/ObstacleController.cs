using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float startingForce;
    private Rigidbody _obstacleRb;

    private void Awake()
    {
        _obstacleRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        var size = Random.Range(0.5f, 1f);
        transform.localScale *= size;
        _obstacleRb.AddForce(Vector3.back * startingForce, ForceMode.Impulse);       
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        _obstacleRb.velocity= Vector3.zero;
        _obstacleRb.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        if (transform.position.z < -50)
        {
            gameObject.SetActive(false);
        }
    }
}
