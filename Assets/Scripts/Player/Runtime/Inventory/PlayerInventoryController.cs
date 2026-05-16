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
        [SerializeField] private ObjectDataBank bank;
        [SerializeField] private Vector2Int size = new Vector2Int(8, 3);
        
        private Inventory inventory = new();
        public Action<Inventory> onInventoryOpened;

        public override void Awake()
        {
            base.Awake();
            InitializeInventory();
            inventory.onInventoryOpened += ExecuteOnInventoryOpened;
        }
        
        private void InitializeInventory()
        {
            inventory.name = "PlayerInventory";
            inventory.size = size;
            string content = InventorySave.InventoryLoad(inventory);
        
            string[] lines = content.Split('\n');
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(':');

                if (!uint.TryParse(parts[0], out uint itemID))
                    continue;

                ObjectData data = ObjectDataBank.Instance.GetObjectDataFromBank(itemID);

                if (data != null)
                    inventory.AddToInventory(data, uint.Parse(parts[2]));
                else 
                    Debug.LogError($"Could not parse item ID {itemID}");
            }
        }

        public void OnInventoryOpened(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                inventory.InvokeOnInventoryOpened();
            }
        }

        private void ExecuteOnInventoryOpened(Inventory _)
        {
            onInventoryOpened?.Invoke(inventory);
        }
        
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
