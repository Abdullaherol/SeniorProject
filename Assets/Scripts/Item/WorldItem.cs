using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour//Word item class
{
    public string itemName;//word 

    private void Start()//Start function
    {
        var itemManager = ItemManager.Instance;//get itemManager instance
        
        itemManager.AddItem(gameObject,this);//add item

        gameObject.layer = 6;//set layer to 6
    }
}
