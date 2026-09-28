using Lucyana.DragSystem;
using UnityEngine;

namespace Lucyana.InventorySystem.UI
{
    [RequireComponent(typeof(InventorySlotUI))]
    public class InventoryDropReceiver : DropUIReceiver
    {
        public override bool CanDrop()
        {
            return GetCurrentDragUI() == null;
        }

        protected override bool OnDrop(DragUI previous, DragUI newDrag)
        {
            InventoryData previousData;
            if (previous != null)
            {
                previousData = previous.GetComponent<InventorySlotUI>().GetCurrentInventoryData();
                
            }

            return true;
        }
    }
}