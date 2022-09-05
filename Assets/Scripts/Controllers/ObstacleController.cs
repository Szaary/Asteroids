using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float startingForce;
    private Rigidbody obstableRB;

    void Start()
    {
        obstableRB = GetComponent<Rigidbody>();
        obstableRB.AddForce(Vector3.back * startingForce, ForceMode.Impulse);       
    }
}
