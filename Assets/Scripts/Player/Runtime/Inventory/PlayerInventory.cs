using System.Collections.Generic;
using System.IO;
using Lucyana.Objects;
using Lucyana.Objects.Items;
using UnityEngine;

namespace Lucyana.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private Dictionary<ObjectData, int> inventory = new ();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Item item))
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

        public void AddToInventory(ObjectData data, int quantity)
        {
            inventory[data] = quantity;
            PrintInventoryIntoFile();
        }

        private void PrintInventoryIntoFile()
        {
            string path = Application.dataPath + "/inventory.txt";
            if (!File.Exists(path))
            {
                Debug.LogWarning("Inventory file does not exist.");
            }
            string inventoryContent = "";
            foreach (var item in inventory)
            {
                inventoryContent += $"{item.Key.ID}:{item.Key.NameProduct}:{item.Value}\n";
            }
            File.WriteAllText(path, inventoryContent);
        }

        public void ClearInventory()
        {
            inventory.Clear();
        }
    }
}
