using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu_Exit", menuName = "Menu/Exit")]
public class Exit : MenuData
{
    public override (Resource, int) GetCost()
    {
        return (resource, unlockCost);
    }

    public override bool CanShow(GameObject target)
    {
        return true;
    }

    public override void Apply(GameObject target)
    {
        GameManager.Instance.StartCoroutine(CloseApplication());

    }

    private IEnumerator CloseApplication()
    {
        yield return new WaitForSecondsRealtime(0.3f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}