using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lucyana.InventorySystem.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI quantityText;
        
        public uint ID { get; private set; }
        
        public void Initialize(InventoryData inventoryData, uint id)
        {
            if (inventoryData == null) return;

            icon.sprite = inventoryData.objectData.Icon;
            quantityText.text = inventoryData.quantity.ToString();
            ID = id;
        }
    }
}
