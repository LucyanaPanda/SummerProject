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
            gameObject.SetActive(true);
            
            if (inventoryData == null)
            {
                BlankSlot();
                return;
            }

            icon.sprite = inventoryData.objectData.Icon;
            icon.color = Color.white;
            quantityText.text = inventoryData.quantity.ToString();
        }

        public void BlankSlot()
        {
            icon.sprite = null;
            icon.color = Color.lightGray;
            quantityText.text = "";
        }
    }
}
