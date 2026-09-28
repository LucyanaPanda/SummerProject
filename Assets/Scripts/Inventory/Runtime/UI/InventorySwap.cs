
namespace Lucyana.InventorySystem.UI
{
    public static class InventorySwap
    {
        public static void Swap(InventorySlotUI slot1, InventorySlotUI slot2)
        {
            if (slot1.GetCurrentInventoryData().inventory != slot2.GetCurrentInventoryData().inventory)
            {
                Inventory temp = slot1.GetCurrentInventoryData().inventory;
                slot1.GetCurrentInventoryData().UpdateInventory(slot2.GetCurrentInventoryData().inventory);
                
            }
        }
    }
}