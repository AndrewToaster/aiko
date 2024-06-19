using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickUpSystem : InteractableBase
{
    private PickUpSystem pickUpSystem;

    public override void OnInteract(GameObject character)
    {
        Debug.Log("PickUpSystem Interaction");
        pickUpSystem = character.GetComponent<PickUpSystem>();
        Item item = GetComponent<Item>();
        if (pickUpSystem != null) pickUpSystem.AddToInventory(item);
    }
}
