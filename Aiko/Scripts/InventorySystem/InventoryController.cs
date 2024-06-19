using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryManager inventoryUI;
    [SerializeField] public InventorySO inventoryData;
    [SerializeField] private GameObject itemPrefab;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        inventoryData.OnEmptyInvetoryItemSet += InventoryData_OnEmptyInvetoryItemSet;
        foreach (InventoryItem item in initialItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            inventoryData.AddItem(item);
        }
    }

    private void InventoryData_OnEmptyInvetoryItemSet(int itemIndex, InventoryItem inventoryItem)
    {
        inventoryUI.ResetSpecificItem(itemIndex);
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventory(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        this.inventoryUI.OnFirstActionClicked += InventoryUI_OnFirstActionClicked;
        this.inventoryUI.OnSecondActionClicked += InventoryUI_OnSecondActionClicked;
    }

    private void InventoryUI_OnSecondActionClicked(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        /*
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, inventoryItem.quantity);
        }
        */

        inventoryData.RemoveItem(itemIndex, inventoryItem.quantity);

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformSecondAction(gameObject, inventoryItem.itemState, inventoryItem.item, inventoryItem,itemPrefab);
            Debug.Log("Drop1");
        }
    }

    private void InventoryUI_OnFirstActionClicked(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
        }
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty) return;
        inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        inventoryData.SwapItems(itemIndex_1, itemIndex_2);
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
        string description = PrepareDescription(inventoryItem);
        inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description);
    }

    public string PrepareDescription(InventoryItem inventoryItem)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(inventoryItem.item.Description);
        sb.AppendLine();
        for (int i = 0; i < inventoryItem.itemState.Count; i++)
        {
            sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName}" + $": {inventoryItem.itemState[i].value} / " + $"{inventoryItem.item.DefaultParametersList[i].value}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
