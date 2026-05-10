using System;
using UnityEngine;
using Lucyana.Objects;
using Lucyana.InventorySystem;
using Lucyana.Objects.Items;
using Lucyana.Utilities;
using UnityEngine.InputSystem;

namespace Lucyana.Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerInventoryController : Singleton<PlayerInventoryController>
    {    
        public event Action<Inventory> onInventoryOpened;
        
        [SerializeField] private ObjectDataBank bank;
        [SerializeField] private Vector2Int size = new Vector2Int(8, 3);
        
        private Inventory inventory = new();

        public override void Awake()
        {
            InitializeInventory();
        }
        
        private void InitializeInventory()
        {
            inventory.name = "PlayerInventory";
            inventory.size = size;
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

        public void OnInventoryOpened(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onInventoryOpened?.Invoke(inventory);
            }
        }
    
        #region Helpers
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
        #endregion
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Item item))
            {
                if (inventory.AddToInventory(item.GetData()))
                    item.Loot();
            }
        }
    }
}
