using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    [SerializeField] private string name;
    [SerializeField] private bool isInteractable;
    [SerializeField] private bool holdToInteract;
    [SerializeField] private float holdDuration;

    public string Name { get { return name; } set { name = value; } }

    public bool IsInteractable => isInteractable;

    public bool HoldToInteract => holdToInteract;

    public float HoldDuration => holdDuration;

    public virtual void OnInteract(GameObject character)
    {
        Debug.Log("base class interaction");
    }
}
