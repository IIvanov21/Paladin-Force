using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(var item in inventory)
        {
            Debug.Log("Inventory contains: " + item.itemName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
