using UnityEngine;

public class SystemsFacade : Singleton<SystemsFacade>
{
    public GameManager gameManager;
    public ResourcesManager resourceManager;
    
    public AudioSource music;
    
    public HUD hud;
    public UiCards uiCards;
    
    public WaveManager waveManager;
    public GameObject player;
    
}
