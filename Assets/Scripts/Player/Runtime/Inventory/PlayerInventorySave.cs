using System.IO;
using UnityEngine;
using Lucyana.Objects;

namespace Lucyana.Player
{
    [RequireComponent(typeof(PlayerInventory))]
    public class PlayerInventorySave : MonoBehaviour
    {
        PlayerInventory playerInventory;
        [SerializeField] private ObjectDataBank bank;

        private void Awake()
        {
            playerInventory = GetComponent<PlayerInventory>();
            InventoryLoad();
        }

        private void InventoryLoad()
        {
            //It will load the inventory from the save file but there is no notion of emplacement yet
            playerInventory.ClearInventory();
            string path = Application.dataPath + "/inventory.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                return;
            }

            string content = File.ReadAllText(path);
            string[] lines = content.Split('\n');
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(':');
                    ObjectData data = GetObjectDataFromBank(parts[1].Trim());
                    if (data != null)
                        playerInventory.AddToInventory(data, int.Parse(parts[2]));
                }
            }
        }

        private ObjectData GetObjectDataFromBank(string nameProduct)
        {
            if (string.IsNullOrEmpty(nameProduct))
            {
                Debug.LogWarning("NameProduct were invalid.");
                return null;
            }

            foreach (ObjectData data in bank.allObjectsData)
            {
                if (data.NameProduct == nameProduct)
                {
                    return data;
                }
            }
            return null;
        }
    }
}
