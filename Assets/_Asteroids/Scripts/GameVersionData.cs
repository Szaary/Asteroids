using UnityEngine;

//[CreateAssetMenu(fileName = "GameVersionData", menuName = "GameVersionData", order = 1)]
public class GameVersionData : ScriptableObject
{
    public ProjectStatus projectStatus;
    public ProjectPhase projectPhase;
    public string buildName;
}