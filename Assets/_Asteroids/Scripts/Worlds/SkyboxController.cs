using System.Collections;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    private static readonly int Rotation = Shader.PropertyToID("_Rotation");
    [SerializeField] private float cameraRotation;

    private void Awake()
    {
        WaveManager.WaveSpawned += OnWaveSpawned;
    }

    private void OnWaveSpawned(int wave, int finalWave)
    {
        StartCoroutine(RotateSkybox(40));
    }

    private IEnumerator RotateSkybox(int rotation)
    {
        for (int i = 0; i < rotation; i++)
        {
            yield return new WaitForSecondsRealtime(0.06f);
            RenderSettings.skybox.SetFloat(Rotation, cameraRotation);
            cameraRotation += 0.1f;
        }
    }

    private void OnDestroy()
    {
        WaveManager.WaveSpawned -= OnWaveSpawned;
    }
}