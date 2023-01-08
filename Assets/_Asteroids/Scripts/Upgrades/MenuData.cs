using UnityEngine;

public abstract class MenuData : ScriptableObject
{
    [SerializeField] protected string title;
    [SerializeField] protected string description;
    [SerializeField] protected bool loopInMenu;
    
    [SerializeField] protected Resource resource;
    [SerializeField] protected int unlockCost;
    public virtual bool LoopInMenu()
    {
        return GameManager.State == GameState.Menu && loopInMenu;
    }

//    public abstract bool ProcessResource();
    public abstract (Resource, int) GetCost(); 
    public virtual string GetTitle() => title;
    public virtual string GetDescription() => description;

    public abstract bool CanShow(GameObject target);
    public abstract void Apply(GameObject target);
}