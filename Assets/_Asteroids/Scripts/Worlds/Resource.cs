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
        Value = initialAmount;
        ui.SetTitle(name);
    }

    public bool Enable
    {
        get => ui.gameObject.activeInHierarchy;
        set => ui.gameObject.SetActive(value);
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