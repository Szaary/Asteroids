using UnityEngine;

[CreateAssetMenu(fileName = "Menu_Music", menuName = "Menu/Music")]
public class Music : MenuData
{
    [SerializeField] private string turnOn;
    [SerializeField] private string turnOnDescription;

    public override (Resource, int) GetCost()
    {
        return (resource, unlockCost);
    }

    public override string GetTitle()
    {
        return SystemsFacade.Instance.music.isPlaying ? title : turnOn;
    }

    public override string GetDescription()
    {
        return SystemsFacade.Instance.music.isPlaying ? description : turnOnDescription;
    }

    public override bool CanShow(GameObject target)
    {
        return true;
    }

    public override void Apply(GameObject target)
    {
        if (SystemsFacade.Instance.music.isPlaying)
        {
            SystemsFacade.Instance.music.Stop();
        }
        else
        {
            SystemsFacade.Instance.music.Play();
        }
    }
}