using Lucyana.InventorySystem;
using Lucyana.InventorySystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Lucyana.Player.UI
{
    public class PlayerInventoryUI : InventoryUI
    {
        [Header("UI Elements")] 
        [SerializeField] private Button inventoryCloseButton;
        
        [SerializeField] private PlayerInventoryController inventoryController;
        [SerializeField] private PlayerMovement playerMovement;

        private void Start()
        {
            inventoryController.onInventoryOpened += ToggleInventoryInterface;
            inventoryCloseButton.onClick.AddListener(HideInventoryInterface);
            HideInventoryInterface();
        }

        private void ToggleInventoryInterface(Inventory inventory)
        {
            isInterfaceOpened = !isInterfaceOpened;
            if (isInterfaceOpened)
            {
                playerMovement.CanMove = false;
                ShowInventoryInterface();
                DisplayInventory(inventory);
            }
            else
            {
                playerMovement.CanMove = true;
                HideInventoryInterface();
            }
        }
    }
}