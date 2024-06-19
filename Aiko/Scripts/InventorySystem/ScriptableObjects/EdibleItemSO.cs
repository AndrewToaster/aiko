using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
{
    [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();
    [SerializeField] private List<ModifierData> secondButtonModifiersData = new List<ModifierData>();

    public string ActionName => "Eat";

    public AudioClip actionSFX {get; private set; }

    public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        foreach (ModifierData data in modifiersData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }
        return true;
    }

    public bool PerformSecondAction(GameObject character, List<ItemParameter> itemState, ItemSO itemSO, InventoryItem inventoryItem,GameObject itemPrefab)
    {
        return Drop(character, itemState, itemSO, inventoryItem, itemPrefab);
    }
}

public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }
    public AudioClip actionSFX { get; }
    bool PerformAction(GameObject character, List<ItemParameter> itemState);
    bool PerformSecondAction(GameObject character, List<ItemParameter> itemState, ItemSO itemSO, InventoryItem inventoryItem, GameObject itemPrefab);
}

[Serializable]
public class ModifierData
{
    public CharacterStatModifierSO statModifier;
    public float value;
}
