using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventoryDatabase", menuName = "Inventory/Database", order = 2)]
public class InventoryDatabase : ScriptableObject {

    public List<Items> itemDatabase = new List<Items>();

    public bool SearchID(string id)
    {
        foreach(var c in itemDatabase)
        {
            if(c.hashID == id)
            {
                return true;
            }
        }
        return false;
    }

    public List<Items> GetItems()
    {
        return itemDatabase;
    }

    public void AddItem(Items newItem)
    {
        itemDatabase.Add(newItem);
    }

    public void RemoveItem(Items newItem)
    {
        itemDatabase.Remove(newItem);
    }
}