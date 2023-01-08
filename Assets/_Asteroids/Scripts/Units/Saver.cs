using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Saver
{
    public static void SavePurchased(List<MenuData> purchased)
    {
        Debug.Log("Saving Purchases");
        var save = new PurchaseSave();
        foreach (var upgrade in purchased)
        {
            var assetGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(upgrade));
            save.guid.Add(assetGuid);
        }

        var json = JsonUtility.ToJson(save);

        Save("Purchased", json);
    }


    public static void LoadPurchased(List<MenuData> purchased)
    {
        Debug.Log("Loading Purchases");
        var guids = Load("Purchased");

        if (guids == "") return;
        var save = JsonUtility.FromJson<PurchaseSave>(guids);
        foreach (var data in save.guid)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(data);
            var scriptable = AssetDatabase.LoadAssetAtPath<MenuData>(assetPath);
            purchased.Add(scriptable);
        }
    }


    public static void SaveResources(Dictionary<Resource, ResourceData> resourceData)
    {
        Debug.Log("Saving Resources");
        var save = new ResourceSave();
        foreach (var resource in resourceData)
        {
            var assetGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(resource.Key));
            save.resources.Add(new ResourceSave.Data
            {
                guid = assetGuid,
                amount = resource.Value.Value
            });
        }

        var json = JsonUtility.ToJson(save);

        Save("Resources", json);
    }

    public static void LoadResources(Dictionary<Resource, ResourceData> resourceData)
    {
        var guids = Load("Resources");

        Debug.Log("Loading Resources");
        if (guids == "") return;
        var save = JsonUtility.FromJson<ResourceSave>(guids);
        foreach (var data in save.resources)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(data.guid);
            var scriptable = AssetDatabase.LoadAssetAtPath<Resource>(assetPath);
            resourceData[scriptable].Value = data.amount;
        }
    }

    [Serializable]
    public class PurchaseSave
    {
        public List<string> guid = new();
    }

    [Serializable]
    public class ResourceSave
    {
        public List<Data> resources = new();

        [Serializable]
        public class Data
        {
            public string guid;
            public int amount;
        }
    }


    private static string Load(string address)
    {
        var guids = PlayerPrefs.GetString(address);
        return guids;
    }

    private static void Save(string address, string json)
    {
        PlayerPrefs.SetString(address, json);
    }
}