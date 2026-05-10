using Lucyana.Objects;

namespace Lucyana.InventorySystem
{
    public class InventoryData
    {
        public ObjectData objectData;
        public uint quantity;

        public InventoryData(ObjectData objectData, uint quantity)
        {
            this.objectData = objectData;
            this.quantity = quantity;
        }
    }
}