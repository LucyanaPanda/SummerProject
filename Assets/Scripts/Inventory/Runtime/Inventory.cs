using System;
using System.Collections;
using System.Collections.Generic;
using Lucyana.Objects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lucyana.InventorySystem
{
    public class Inventory : IEnumerable<InventoryData>
    {
        public string name;
        public Vector2Int size;
        private Dictionary<uint, InventoryData> inventory = new ();
        private Dictionary<Vector2Int, uint> inventoryPositions = new ();
        
        public event Action<Inventory> onInventoryOpened;
        public int Count => inventory.Count;

        public void InitializeInventoryPosition()
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    inventoryPositions.TryAdd(new Vector2Int(x, y), 0);
                }
            }
        }

        private Vector2Int GetAvailableSlotPosition()
        {
            foreach (KeyValuePair<Vector2Int, uint> pair in inventoryPositions)
            {
                if (pair.Value == 0)
                {
                    Debug.Log($" Key { pair.Key }");
                    return pair.Key;
                };
            }
            return -Vector2Int.one;
        }

        private Vector2Int ReturnValidPosition(Vector2Int position)
        {
            if (-Vector2Int.one == position) return GetAvailableSlotPosition();
            return position;
        }
        
        #region Inventory Managment

        public bool AddToInventory(ObjectData data)
        {
            return AddToInventory(data, -Vector2Int.one);
        }
        
        public bool AddToInventory(ObjectData data, Vector2Int position)
        {
            Vector2Int newPosition = ReturnValidPosition(position);
            if (!inventory.TryAdd(data.ID, new InventoryData(this, data, 1, newPosition)))
            {
                inventory[data.ID].quantity++;
            }
            else
            {
                inventoryPositions[newPosition] = data.ID;
            }

            if (inventory.Count >= size.x * size.y)
                return false;
            
            InventorySave.PrintInventoryIntoFile(this);
            return true;
        }

        public bool AddToInventory(ObjectData data, uint quantity)
        {
            return AddToInventory(data, quantity, -Vector2Int.one);
        }

        public bool AddToInventory(ObjectData data, uint quantity, Vector2Int position)
        {
            Vector2Int newPosition = ReturnValidPosition(position);
            if (!inventory.TryAdd(data.ID, new InventoryData(this, data, quantity, newPosition)))
            {
                inventory[data.ID].quantity += quantity;
            }
            else
            {
                inventoryPositions[newPosition] = data.ID;
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
        
         #endregion

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
