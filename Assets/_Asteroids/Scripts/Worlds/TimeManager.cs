using DG.Tweening;
using UnityEngine;

public static class TimeManager
{
    public static void StopTime()
    {
        DOVirtual.Float(1, 0, 0.5f, ChangeTime).SetEase(Ease.InQuint).SetUpdate(true);;
    }

    public static void ResumeTime()
    {
        DOVirtual.Float(0, 1, 0.5f, ChangeTime).SetEase(Ease.InQuint).SetUpdate(true);;
    }
    
    
    private static void ChangeTime(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
