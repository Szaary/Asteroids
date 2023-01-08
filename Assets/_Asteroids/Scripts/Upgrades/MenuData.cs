using UnityEngine;

public abstract class MenuData : ScriptableObject
{
    [SerializeField] protected string title;
    [SerializeField] protected string description;
    [SerializeField] protected bool loopInMenu;
    
    [SerializeField] protected Resource resource;
    [SerializeField] protected int unlockCost;
    
    public virtual bool LoopInMenu() => GameManager.State == GameState.Menu && loopInMenu;
    public abstract (Resource, int) GetCost(); 
    public virtual string GetTitle() => title;
    public virtual string GetDescription() => description;
    public abstract bool CanShow(GameObject target);
    public virtual bool CanApply() => ResourcesManager.Instance.TryBuy(resource, unlockCost);
    public abstract void Apply(GameObject target);
}