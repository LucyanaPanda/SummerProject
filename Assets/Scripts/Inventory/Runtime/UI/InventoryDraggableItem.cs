using Lucyana.DragSystem;
using UnityEngine;

namespace Lucyana.InventorySystem.UI
{
    [RequireComponent(typeof(InventorySlotUI))]
    public class InventoryDraggableItem : DragUI
    {
        private InventorySlotUI inventorySlotUI;
        
        private void Start()
        {
            inventorySlotUI = GetComponent<InventorySlotUI>();
        }
        
        protected override bool CanDrag()
        {
            return inventorySlotUI != null && !inventorySlotUI.IsEmpty();
        }
    }
}