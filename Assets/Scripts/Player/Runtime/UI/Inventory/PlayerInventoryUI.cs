using System;
using System.Collections.Generic;
using Lucyana.InventorySystem;
using Lucyana.InventorySystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Lucyana.Player.UI
{
    public class PlayerInventoryUI : InventoryUI
    {
        [Header("UI Elements")] 
        [SerializeField] private GameObject inventoryGO;
        [SerializeField] private Button inventoryCloseButton;
        
        private PlayerInventoryController inventoryController;
        private bool isInterfaceOpened;

        private void Start()
        {
            inventoryController = PlayerInventoryController.Instance;
            inventoryController.onInventoryOpened += ToggleInventoryInterface;

            inventoryCloseButton.onClick.AddListener(HideInventoryInterface);
            
            HideInventoryInterface();
        }

        private void ToggleInventoryInterface(Inventory inventory)
        {
            isInterfaceOpened = !isInterfaceOpened;
            if (isInterfaceOpened)
            {
                ShowInventoryInterface();
                DisplayInventory(inventory);
            }
            else
            {
                HideInventoryInterface();
            }
        }

        private void ShowInventoryInterface()
        {
            inventoryGO.SetActive(true);
        }

        private void HideInventoryInterface()
        {
            inventoryGO.SetActive(false);
            isInterfaceOpened = false;
        }
    }
}