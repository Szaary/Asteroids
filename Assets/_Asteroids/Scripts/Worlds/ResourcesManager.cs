using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private ResourceUI resourcePrefab;
    [SerializeField] private List<Resource> resources;

    private Dictionary<Resource, ResourceData> _resourceData;

    private void Awake()
    {
        _resourceData = new Dictionary<Resource, ResourceData>();

        foreach (var resource in resources)
        {
            var instance = Instantiate(resourcePrefab, transform);
            _resourceData.Add(resource, new ResourceData(
                instance,
                0,
                resource.resourceName
            ));
        }

        GameManager.GameStateChanged += OnGameStateChanged;
    }


    public bool CanBuy(Resource resource, int cost) => _resourceData[resource].Value >= cost;

    public bool TryBuy(Resource resource, int cost)
    {
        if (_resourceData[resource].Value >= cost)
        {
            _resourceData[resource].Value -= cost;
            return true;
        }

        return false;
    }

    public bool Increase(Resource resource, int value)
    {
        _resourceData[resource].Value += value;
        return true;
    }




    private void OnGameStateChanged(GameState state)
    {
        foreach (var resource in _resourceData)
        {
            resource.Value.Enable = state == GameState.Menu;
        }
    }
    
    private void OnDestroy()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }
}