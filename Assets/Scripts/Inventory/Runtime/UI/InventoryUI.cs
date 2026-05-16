using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lucyana.InventorySystem.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] protected List<InventorySlotUI> inventorySlots;

        protected void DisplayInventory(Inventory inventory)
        {
            int index = 0;
            int count = inventorySlots.Count;
            foreach (InventoryData data in inventory)
            {
                if (index == count)
                    break;
                
                inventorySlots[index].Initialize(data);
                index++;
            }

            for (;index < inventorySlots.Count; index++)
            {
                inventorySlots[index].gameObject.SetActive(false);
            }
        }
    }
}