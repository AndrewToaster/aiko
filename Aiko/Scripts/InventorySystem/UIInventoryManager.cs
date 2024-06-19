using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private RectTransform noItemsText;
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    [SerializeField] private UIInventoryDescription inventoryDescription;
    [SerializeField] private MouseFollower mouseFollower;

    private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging, OnFirstActionClicked, OnSecondActionClicked;
    public event Action<int, int> OnSwapItems;

    private UIInventoryItem selectedUIInventoryItem;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        inventoryDescription.ResetDescription();
    }

    public void InitializeInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            AddItem();
        }
    }

    private void Update()
    {
        SetNoItemsText();
    }

    private void AddItem()
    {
        UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
        uiItem.transform.SetParent(contentPanel);
        listOfUIItems.Add(uiItem);
        uiItem.onItemClicked += HandleItemSelection;
        uiItem.onItemBeginDrag += UiItem_onItemBeginDrag;
        uiItem.onItemDroppedOn += UiItem_onItemDroppedOn;
        uiItem.onItemEndDrag += UiItem_onItemEndDrag;
        uiItem.onRightMouseBtnClicked += UiItem_onRightMouseBtnClicked;
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    public void OnFirstActionClick()
    {
        int index = listOfUIItems.IndexOf(selectedUIInventoryItem);
        if (index == -1)
        {
            return;
        }
        OnFirstActionClicked?.Invoke(index);
    }

    public void OnSecondActionClick()
    {
        int index = listOfUIItems.IndexOf(selectedUIInventoryItem);
        if (index == -1)
        {
            return;
        }
        OnSecondActionClicked?.Invoke(index);
    }

    private void UiItem_onRightMouseBtnClicked(UIInventoryItem inventoryItemUI)
    {
        
    }

    private void UiItem_onItemEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDraggedItem();
    }

    private void UiItem_onItemDroppedOn(UIInventoryItem inventoryItemUI)
    {
        // swapping objects
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(inventoryItemUI);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void UiItem_onItemBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1) return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1) return;
        OnDescriptionRequested?.Invoke(index);
        selectedUIInventoryItem = inventoryItemUI;
    }

    private void SetNoItemsText()
    {
        if (noItemsText != null)
        {
            if (listOfUIItems.Count > 0)
            {
                noItemsText.gameObject.SetActive(false);
            }
            else
            {
                noItemsText.gameObject.SetActive(true);
            }
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        inventoryDescription.ResetDescription();
        DeselectAllItems();
        selectedUIInventoryItem = null;
    }

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
        //Debug.Log("deselect all");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        inventoryDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfUIItems[itemIndex].Select();
    }

    public void ResetSpecificItem(int itemIndex)
    {
        listOfUIItems[itemIndex].Deselect();
        inventoryDescription.ResetDescription();
        selectedUIInventoryItem = null;
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            //item.Deselect();
        }
        //selectedUIInventoryItem = null;
        //Debug.Log("reset all items");
    }
}
