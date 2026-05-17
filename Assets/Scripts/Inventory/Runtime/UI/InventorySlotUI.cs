using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lucyana.InventorySystem.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI quantityText;

        private Vector2Int positionSlot;
        private InventoryData currentObjectData;
        
        public void OnSlotCreated(Vector2Int position)
        {
            positionSlot = position;
            gameObject.name = $"Slot_{position.x}_{position.y}";
        }
        
        public void Initialize(InventoryData inventoryData)
        {            
            gameObject.SetActive(true);
            
            if (inventoryData == null)
            {
                Debug.LogWarning("InventoryData is null for slot " + positionSlot);
                BlankSlot();
                return;
            }
            
            currentObjectData = inventoryData;
            icon.sprite = inventoryData.objectData.Icon;
            icon.color = Color.white;
            quantityText.text = inventoryData.quantity.ToString();
        }

        public void BlankSlot()
        {
            currentObjectData = null;
            
            icon.sprite = null;
            icon.color = Color.lightGray;
            quantityText.text = "";
        }
    }
}
