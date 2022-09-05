using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAsteroids : MonoBehaviour
{
    private static GameManagerAsteroids _instance;
    public static GameManagerAsteroids Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManagerAsteroids");
                go.AddComponent<GameManagerAsteroids>();
            }
            return _instance;
        }

    }
    
    public float borderX { get; set; }
    public float borderY { get; set; }
    public float borderZ { get; set; }
    public float borderForce { get; set; }
    public float asteroidFrequency { get; set; }
    [Range(0, 50)]
    public float maxPlayerVelocityY;
    [Range(0, 50)]
    public float maxPlayerVelocityX;
    public float enemySpawnFrequency { get; set; }
    public float enemyStopTime { get; set; }
    [Range(0, 9)]
    public int numberOfWeapons;
    public int numberOfUpgrades { get; set; }
    public int enemiesKilled { get; set; }
    public int waveNumber { get; set; }
    private float cameraRotation { get; set; }


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
        enemyStopTime = 4;
        numberOfWeapons = 1;
        enemiesKilled = 0;
        waveNumber = 0;
        numberOfUpgrades = 0;
        
    }



    public static void RotateCamera(float SkyboxRotationY)
    {
        RenderSettings.skybox.SetFloat("_Rotation", SkyboxRotationY);
    }

    public static IEnumerator IRotateSkybox(int rotation)
    {
        for (int i = 0; i < rotation; i++)
        {
            yield return new WaitForSeconds(0.06f);
            GameManagerAsteroids.RotateCamera(GameManagerAsteroids.Instance.cameraRotation);
            GameManagerAsteroids.Instance.cameraRotation += 0.1f;
        }
    }


}
