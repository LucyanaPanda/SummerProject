using UnityEngine;
using Lucyana.Objects;
using Lucyana.InventorySystem;
using Lucyana.Objects.Items;

namespace Lucyana.Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerInventoryController : MonoBehaviour
    {    
        [SerializeField] private ObjectDataBank bank;
        private Inventory inventory = new();

        private void Awake()
        {
            InitializeInventory();
        }
        
        private void InitializeInventory()
        {
            inventory.name = "PlayerInventory";
            string content = InventorySave.InventoryLoad(inventory);
        
            string[] lines = content.Split('\n');
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(':');
                    ObjectData data = GetObjectDataFromBank(parts[1].Trim());
                    if (data != null)
                        inventory.AddToInventory(data, uint.Parse(parts[2]));
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
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Item item))
            {
                ObjectData data = item.Loot();
                inventory.AddToInventory(data);
            }
        }
    }
}
