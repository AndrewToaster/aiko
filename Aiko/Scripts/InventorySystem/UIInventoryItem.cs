using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject quantityParentBackground;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image borderImage;

    private Color defaultBorderImageColor;
    public event Action<UIInventoryItem> onItemClicked, onItemDroppedOn, onItemBeginDrag, onItemEndDrag, onRightMouseBtnClicked;
    private bool empty = true;

    private void Awake()
    {
        defaultBorderImageColor = borderImage.color;
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
        quantityParentBackground.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {
        borderImage.color = defaultBorderImageColor;
        //Debug.Log("deselect item");
    }

    public void SetData(Sprite sprite, int quantity)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        quantityText.text = quantity.ToString();
        if (quantity != 0) quantityParentBackground.SetActive(true);
        empty = false;
    }

    public void Select()
    {
        borderImage.color = Color.red; // idk what rn, desingn later
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            onRightMouseBtnClicked?.Invoke(this);
        }
        else
        {
            onItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty) return;
        onItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        onItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
