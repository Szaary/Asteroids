using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfArea : MonoBehaviour
{
    void Update()
    {

        if (gameObject.CompareTag("Obstacle") && transform.position.z < -50)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.CompareTag("Enemy") && transform.position.z < -5)
        {
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Missile") && transform.position.z > 220)
        {
            Destroy(gameObject);
        }
    }
}

