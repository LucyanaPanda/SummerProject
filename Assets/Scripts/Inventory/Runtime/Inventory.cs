using System.Collections;
using System.Collections.Generic;
using Lucyana.Objects;

namespace Lucyana.InventorySystem
{
    public class Inventory : IEnumerable<InventoryData>
    {
        public string name;
        private Dictionary<uint, InventoryData> inventory = new ();

        public void AddToInventory(ObjectData data)
        {
            if (!inventory.TryAdd(data.ID, new InventoryData(data, 1)))
            {
                inventory[data.ID].quantity++;
            }
            
            InventorySave.PrintInventoryIntoFile(this);
        }

        public void AddToInventory(ObjectData data, uint quantity)
        {
            if (!inventory.TryAdd(data.ID, new InventoryData(data, quantity)))
            {
                inventory[data.ID].quantity += quantity;
            }
            InventorySave.PrintInventoryIntoFile(this);
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
