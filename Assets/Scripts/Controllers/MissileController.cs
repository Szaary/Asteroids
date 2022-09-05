using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private float startingForce;
    private Rigidbody missileRb;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");        
        missileRb = GetComponent<Rigidbody>();        
        missileRb.AddForce(new Vector3(-1.4f*player.transform.rotation.z,0,1) 
            * startingForce, ForceMode.Impulse);
    }  
}
