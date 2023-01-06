using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManagerAsteroids");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public float borderX { get; set; }
    public float borderY { get; set; }
    public float borderZ { get; set; }
    public float borderForce { get; set; }
    public float asteroidFrequency { get; set; }
    [Range(0, 50)] public float maxPlayerVelocityY;
    [Range(0, 50)] public float maxPlayerVelocityX;
    public float enemySpawnFrequency { get; set; }


    private void Awake()
    {
        _instance = this;

        borderX = 40;
        borderY = 40;
        borderZ = 120;
        borderForce = 1;
        asteroidFrequency = 0.1f;
        maxPlayerVelocityY = 20;
        maxPlayerVelocityX = 20;
        enemySpawnFrequency = 1;
        
    }
}