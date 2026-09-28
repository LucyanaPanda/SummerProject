using Lucyana.Objects;
using UnityEngine;

namespace Lucyana.InventorySystem
{
    public class InventoryData
    {
        public Inventory inventory;
        public ObjectData objectData;
        public uint quantity;
        public Vector2Int position;

        public InventoryData(Inventory inventory, ObjectData objectData, uint quantity, Vector2Int position)
        {
            this.inventory = inventory;
            this.objectData = objectData;
            this.quantity = quantity;
            this.position = position;
        }

        public void UpdateInventoryPosition(Vector2Int position)
        {
            this.position = position;
        }

        public void UpdateInventory(Inventory newInventory)
        {
            if (!inventory.RemoveFromInventory(objectData, out quantity)) return;
            
            if (newInventory.AddToInventory(objectData, quantity))
            {
                inventory = newInventory;
            }
            else
            {
                Debug.LogError($"[InventoryData] couldn't add this item {objectData.name} of quantity {quantity} to inventory ");
            }
        }
    }
}