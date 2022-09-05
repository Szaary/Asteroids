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

    [SerializeField] private TextMeshProUGUI _weapons;
    [SerializeField] private TextMeshProUGUI _enemiesKilled;
    [SerializeField] private TextMeshProUGUI _wave;
    [SerializeField] private TextMeshProUGUI _upgrades;

    [SerializeField] private Light _shotLight;
    [SerializeField] private Light _explodeLight;

    [SerializeField] private int _destroyAnimationTime;

    private void Awake()
    {
        EventBroker.ShotAction += StartIFlashCorutine;
        EventBroker.ExplodeAction += StartIFlashExplodeCorutine;

       _instance = this;

        _weapons = GameObject.Find("Weapons").GetComponent<TextMeshProUGUI>(); ;
        _enemiesKilled = GameObject.Find("Enemies Killed").GetComponent<TextMeshProUGUI>();
        _wave = GameObject.Find("Wave").GetComponent<TextMeshProUGUI>();
        _upgrades = GameObject.Find("Upgrades").GetComponent<TextMeshProUGUI>();

        _shotLight = GameObject.Find("ShotLight").GetComponent<Light>();
        _explodeLight = GameObject.Find("ExplodeLight").GetComponent<Light>();

        _destroyAnimationTime = 1;
    }
    private void OnDisable()
    {
        EventBroker.ShotAction -= StartIFlashCorutine;
        EventBroker.ExplodeAction -= StartIFlashExplodeCorutine;

        //StopAllCoroutines();
    }

    public static void ShowStats()
    {
        
        HUD.Instance._weapons.GetComponent<TextMeshProUGUI>().text = "Weapons: " + GameManagerAsteroids.Instance.numberOfWeapons;
        HUD.Instance._enemiesKilled.text = "Enemies Killed: " + GameManagerAsteroids.Instance.enemiesKilled;
        HUD.Instance._wave.text = "Wave: " + GameManagerAsteroids.Instance.waveNumber;
        HUD.Instance._upgrades.text = "Upgrades: " + GameManagerAsteroids.Instance.numberOfUpgrades;
    }
    private void StartIFlashCorutine()
    {
        StartCoroutine(IFlashShotLight());
    }
    private static IEnumerator IFlashShotLight()
    {
        HUD.Instance._shotLight.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        HUD.Instance._shotLight.gameObject.SetActive(false);
    }


    private void StartIFlashExplodeCorutine()
    {
        StartCoroutine(IFlashExplodeLight());
    }
    public static IEnumerator IFlashExplodeLight()
    {

        HUD.Instance._explodeLight.intensity = 0.6f;
        yield return new WaitForSeconds(HUD.Instance._destroyAnimationTime - 0.8f);
        HUD.Instance._explodeLight.intensity = 0;

    }

}
