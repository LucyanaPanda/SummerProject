using System;
using System.Collections.Generic;
using Lucyana.InventorySystem;
using Lucyana.InventorySystem.UI;
using UnityEngine;

namespace Lucyana.Player.UI
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [Header("Inventory UI")]
        [SerializeField] private GameObject inventoryGO;
        [SerializeField] private InventorySlotUI inventorySlotPrefab;
        
        private List<InventorySlotUI> inventorySlots;
        
        private PlayerInventoryController inventoryController;
        private bool isInterfaceOpened;

        private void Start()
        {
            inventoryController = PlayerInventoryController.Instance;
            inventoryController.onInventoryOpened += ToggleInventoryInterface;
        }

        private void ToggleInventoryInterface(Inventory inventory)
        {
            isInterfaceOpened = !isInterfaceOpened;
            if (isInterfaceOpened)
            {
                
            }
            else
            {
                
            }
        }

        private void CreateInventorySlots()
        {
            
        }
    }
}