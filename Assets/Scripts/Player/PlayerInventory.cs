using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ObjectData, int> inventory = new Dictionary<ObjectData, int>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Item>(out Item item))
        {
            ObjectData data = item.Loot();
            AddToInventory(data);
        }
    }

    private void AddToInventory(ObjectData data)
    {
        if (inventory.ContainsKey(data))
        {
            inventory[data]++;
        }
        else
        {
            inventory[data] = 1;
        }
        PrintInventoryIntoFile();
    }

    private void PrintInventoryIntoFile()
    {
        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/NewTextFile.txt", true))
        {
            sw.WriteLine("This is a new text file!");
            string inventoryContent = "Current Inventory:\n";
            foreach (var item in inventory)
            {
                inventoryContent += $"{item.Key.NameProduct}: {item.Value}\n";
            }
            sw.WriteLine(inventoryContent);
        }
    }
}
