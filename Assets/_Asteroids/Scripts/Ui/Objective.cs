using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public class Objective : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progress;
    private Action cleanupAction;
    
    public void SetActive(bool isActive)
    {
        progress.gameObject.SetActive(isActive);
    }

    public void Set(string objProgress)
    {
        if (progress.text == objProgress) return;
        progress.text = objProgress;
        progress.gameObject.transform.DOShakeScale(0.3f, 0.1f, 10, 0f, true).SetUpdate(true);
    }
    
    public void SetDestroy(Action action)
    {
        cleanupAction = action;
    }

    private void OnDestroy()
    {
        cleanupAction?.Invoke();
    }
}