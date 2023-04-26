using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    private Dictionary<GameObject, WorldItem> _items = new Dictionary<GameObject, WorldItem>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(GameObject worldObject, WorldItem worldItem)
    {
        _items.Add(worldObject,worldItem);
    }

    public bool GetItem(GameObject worldObject, out WorldItem worldItem)
    {
        var result = _items.ContainsKey(worldObject);

        worldItem = null;

        if (result)
        {
            worldItem = _items[worldObject];
        }

        return result;
    }
}