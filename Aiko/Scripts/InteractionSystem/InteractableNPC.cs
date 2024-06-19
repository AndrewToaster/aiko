using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string name;

    public string Name => name;

    public bool IsInteractable => throw new System.NotImplementedException();

    public bool HoldToInteract => throw new System.NotImplementedException();

    public float HoldDuration => throw new System.NotImplementedException();

    public void Interact()
    {
        Debug.Log("Interact " + Name);
    }

    public void OnInteract(GameObject character)
    {
        throw new System.NotImplementedException();
    }
}
