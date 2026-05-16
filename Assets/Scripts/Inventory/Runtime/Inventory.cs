using System;
using System.Collections;
using System.Collections.Generic;
using Lucyana.Objects;
using UnityEngine;

namespace Lucyana.InventorySystem
{
    public class Inventory : IEnumerable<InventoryData>
    {
        public string name;
        public Vector2Int size;
        private Dictionary<uint, InventoryData> inventory = new ();
        
        public event Action<Inventory> onInventoryOpened;
        public int Count => inventory.Count;

        public bool AddToInventory(ObjectData data)
        {
            if (!inventory.TryAdd(data.ID, new InventoryData(data, 1)))
            {
                inventory[data.ID].quantity++;
            }

            if (inventory.Count >= size.x * size.y)
                return false;
            
            InventorySave.PrintInventoryIntoFile(this);
            return true;
        }

        public bool AddToInventory(ObjectData data, uint quantity)
        {
            if (!inventory.TryAdd(data.ID, new InventoryData(data, quantity)))
            {
                inventory[data.ID].quantity += quantity;
            }
            
            if (inventory.Count >= size.x * size.y)
                return false;
            
            InventorySave.PrintInventoryIntoFile(this);
            return true;
        }

        public bool RemoveFromInventory(ObjectData data, out uint quantity)
        {
            if (inventory.ContainsKey(data.ID))
            {
                quantity = inventory[data.ID].quantity;
                inventory.Remove(data.ID);
                InventorySave.PrintInventoryIntoFile(this);
                return true;
            }
            
            quantity = 0;
            return false;
        }

        public bool RemoveFromInventory(ObjectData data, uint quantity)
        {
            if (!inventory.ContainsKey(data.ID)) return false;
            
            inventory[data.ID].quantity -= quantity;
            if (inventory[data.ID].quantity <= 0)
            {
                inventory.Remove(data.ID);
            }
            InventorySave.PrintInventoryIntoFile(this);
            return true;
        }

        public void ClearInventory()
        {
            inventory.Clear();
        }

        public void InvokeOnInventoryOpened()
        {
            onInventoryOpened?.Invoke(this);
        }

        #region IEnumerable
        public IEnumerator<InventoryData> GetEnumerator()
        {
            foreach (InventoryData data in inventory.Values)
            {
                yield return data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
