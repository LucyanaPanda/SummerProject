using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lucyana.InventorySystem.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI quantityText;
        
        public void Initialize(InventoryData inventoryData)
        {
            if (inventoryData == null) return;
            
            gameObject.SetActive(true);
            icon.sprite = inventoryData.objectData.Icon;
            quantityText.text = inventoryData.quantity.ToString();
        }
    }
}
