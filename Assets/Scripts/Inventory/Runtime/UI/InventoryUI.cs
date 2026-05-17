using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lucyana.InventorySystem.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] protected GameObject inventoryPanelGO;
        [SerializeField] protected GridLayoutGroup inventoryContentParent;
        [SerializeField] protected InventorySlotUI inventorySlotPrefab;
        
        protected Dictionary<Vector2Int, InventorySlotUI> inventorySlots = new Dictionary<Vector2Int, InventorySlotUI>();
        protected bool isInterfaceOpened;

        protected void DisplayInventory(Inventory inventory)
        {
            GenerateSlots(inventory.size);
            
            List<Vector2Int> slotsOccupied = new List<Vector2Int>();
            foreach (InventoryData data in inventory)
            {
                inventorySlots[data.position].Initialize(data);
                slotsOccupied.Add(data.position);
            }

            foreach (KeyValuePair<Vector2Int, InventorySlotUI> pair in inventorySlots)
            {
                if (slotsOccupied.Contains(pair.Key)) continue;
                pair.Value.BlankSlot();
            }
        }

        private void GenerateSlots(Vector2Int inventorySize)
        {
            List<Vector2Int> slots = new List<Vector2Int>();
            for (int x = 0; x < inventorySize.x; x++)
            {
                for (int y = 0; y < inventorySize.y; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    slots.Add(position);
                    if (inventorySlots.ContainsKey(position))
                    {
                        inventorySlots[position].gameObject.SetActive(true);
                    }
                    else
                    {
                        InventorySlotUI slot = Instantiate(inventorySlotPrefab, inventoryContentParent.transform);
                        slot.OnSlotCreated(position);
                        inventorySlots[position] = slot;
                    }
                }
            }

            List<Vector2Int> obsoletePositions = inventorySlots.Keys
                .Where(x => !slots.Contains(x)).ToList();

            foreach (Vector2Int position in obsoletePositions)
            {
                Destroy(inventorySlots[position]);
                inventorySlots.Remove(position);
            }
        }
        
        protected void ShowInventoryInterface()
        {
            inventoryPanelGO.SetActive(true);
        }

        protected void HideInventoryInterface()
        {
            inventoryPanelGO.SetActive(false);
            isInterfaceOpened = false;
        }
    }
}