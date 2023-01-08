using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    private static readonly int Rotation = Shader.PropertyToID("_Rotation");
    private float cameraRotation;

    private void Awake()
    {
        cameraRotation = RenderSettings.skybox.GetFloat(Rotation);
        WaveManager.WaveSpawned += OnWaveSpawned;
    }

    private void OnWaveSpawned(int wave, int finalWave)
    {
        var random = Random.Range(-10, 10);
        DOVirtual.Float(cameraRotation, cameraRotation + random, 1, value =>
        {
            RenderSettings.skybox.SetFloat(Rotation, value);
        });
    }

    private void OnDestroy()
    {
        WaveManager.WaveSpawned -= OnWaveSpawned;
    }
}