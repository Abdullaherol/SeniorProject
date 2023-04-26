using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public string itemName;

    private void Start()
    {
        var itemManager = ItemManager.Instance;
        
        itemManager.AddItem(gameObject,this);

        gameObject.layer = 6;
    }
}
