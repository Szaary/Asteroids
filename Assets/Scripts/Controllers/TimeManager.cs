using DG.Tweening;
using UnityEngine;

public static class TimeManager
{
    public static void StopTime()
    {
        Debug.Log("Stopping Time");
        DOVirtual.Float(1, 0, 0.5f, ChangeTime).SetEase(Ease.InQuint).SetUpdate(true);;
    }

    public static void ResumeTime()
    {
        Debug.Log("Resuming Time");
        DOVirtual.Float(0, 1, 0.5f, ChangeTime).SetEase(Ease.InQuint).SetUpdate(true);;
    }
    
    
    private static void ChangeTime(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
