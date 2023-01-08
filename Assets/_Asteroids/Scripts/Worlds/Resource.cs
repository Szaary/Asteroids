using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource_", menuName = "Menu/Resource")]
public class Resource : ScriptableObject
{
    public string resourceName;
}

[Serializable]
public class ResourceData
{
    [SerializeField] private ResourceUI ui;
    [SerializeField] private int amount;
    
    public ResourceData(ResourceUI uiReference, int initialAmount, string name)
    {
        ui = uiReference;
        amount = initialAmount;
        ui.SetTitle(name);
    }

    public bool Enable
    {
        get => ui.enabled;
        set => ui.enabled = value;
    }

    public int Value
    {
        get => amount;
        set
        {
            amount = value;
            ui.SetValue(amount.ToString());
        }
    }
}