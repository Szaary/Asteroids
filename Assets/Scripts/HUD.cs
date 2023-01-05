using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private static HUD _instance;

    public static HUD Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("HUD");
                go.AddComponent<HUD>();
            }

            return _instance;
        }
    }

    [SerializeField] private TextMeshProUGUI _enemiesKilled;
    [SerializeField] private TextMeshProUGUI _wave;


    [SerializeField] private Light _shotLight;
    [SerializeField] private Light _explodeLight;

    [SerializeField] private int _destroyAnimationTime;

    private void Awake()
    {
        EventBroker.ShotAction += StartIFlashCoroutine;
        EventBroker.ExplodeAction += StartIFlashExplodeCoroutine;

        _instance = this;

        _enemiesKilled = GameObject.Find("Enemies Killed").GetComponent<TextMeshProUGUI>();
        _wave = GameObject.Find("Wave").GetComponent<TextMeshProUGUI>();
        _shotLight = GameObject.Find("ShotLight").GetComponent<Light>();
        _explodeLight = GameObject.Find("ExplodeLight").GetComponent<Light>();

        _destroyAnimationTime = 1;
    }

    private void OnDisable()
    {
        EventBroker.ShotAction -= StartIFlashCoroutine;
        EventBroker.ExplodeAction -= StartIFlashExplodeCoroutine;
    }

    private void Update()
    {
        ShowStats();
    }

    private void ShowStats()
    {
        _enemiesKilled.text = "Enemies Killed: " + GameManager.Instance.enemiesKilled;
        _wave.text = "Wave: " + GameManager.Instance.waveNumber;
    }

    private void StartIFlashCoroutine()
    {
        StartCoroutine(FlashShotLight());
    }

    private IEnumerator FlashShotLight()
    {
        _shotLight.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _shotLight.gameObject.SetActive(false);
    }

    private void StartIFlashExplodeCoroutine()
    {
        StartCoroutine(FlashExplodeLight());
    }

    private IEnumerator FlashExplodeLight()
    {
        _explodeLight.intensity = 0.6f;
        yield return new WaitForSecondsRealtime(_destroyAnimationTime - 0.8f);
        _explodeLight.intensity = 0;
    }
}