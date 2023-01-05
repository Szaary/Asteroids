using System;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private float startingForce;
    private Rigidbody missileRb;
    private GameObject player;

    private void Awake()
    {
        missileRb = GetComponent<Rigidbody>();    
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (transform.position.z > 220)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        missileRb.AddForce(new Vector3(-1.4f*player.transform.rotation.z,0,1) 
                           * startingForce, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        missileRb.velocity=Vector3.zero;
        missileRb.angularVelocity = Vector3.zero;
    }
}
