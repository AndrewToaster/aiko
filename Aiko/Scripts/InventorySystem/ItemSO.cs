using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [field: SerializeField] public bool IsStackable {  get; set; }

    public int ID => GetInstanceID();

    [field: SerializeField] public int MaxStackSize { get; set; } = 1;
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField][field: TextArea] public string Description { get; set; }
    [field: SerializeField] public Sprite ItemImage { get; set; }
    [field: SerializeField] public List<ItemParameter> DefaultParametersList { get; set; }

    public virtual bool Drop(GameObject character, List<ItemParameter> itemState, ItemSO itemSO, InventoryItem inventoryItem, GameObject itemPrefab)
    {
        Vector2 pos = character.transform.position;
        GameObject newItemPrefab = Instantiate(itemPrefab, pos, Quaternion.identity);
        newItemPrefab.AddComponent<InteractablePickUpSystem>();
        newItemPrefab.GetComponent<InteractablePickUpSystem>().Name = itemSO.Name;
        Item item = newItemPrefab.GetComponent<Item>();
        if (item != null)
        {
            item.InventoryItem = itemSO;
            item.Quantity = inventoryItem.quantity;
            return true;
        }
        return false;
    }
}

[Serializable]
public struct ItemParameter : IEquatable<ItemParameter>
{
    public ItemParameterSO itemParameter;
    public float value;

    public bool Equals(ItemParameter other)
    {
        return other.itemParameter == itemParameter;
    }
}
