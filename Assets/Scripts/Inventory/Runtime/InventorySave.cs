using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Lucyana.InventorySystem
{
    public static class InventorySave
    {
        public static void PrintInventoryIntoFile(Inventory inventory)
        {
            string inventoryPath = "/" + new string(inventory.name.Where(Char.IsLetterOrDigit).ToArray()) + ".txt";
            string path = Application.dataPath + inventoryPath;
            if (!File.Exists(path))
            {
                Debug.LogWarning("Inventory file does not exist.");
            }
            string inventoryContent = "";
            
            foreach (InventoryData data in inventory)
            {
                inventoryContent += $"{data.objectData.ID}:{data.objectData.NameProduct}:{data.quantity}:{data.position}\n";
            }
            File.WriteAllText(path, inventoryContent);
        }
        
        public static string InventoryLoad(Inventory inventory)
        {
            string inventoryPath = "/" + new string(inventory.name.Where(Char.IsLetterOrDigit).ToArray()) + ".txt";
            string path = Application.dataPath + inventoryPath;
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                Debug.Log("Creating inventory file");
                return string.Empty;
            }

            return File.ReadAllText(path);
        }
    }
}