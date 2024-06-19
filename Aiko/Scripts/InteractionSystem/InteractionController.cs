using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private InteractionManager interactionManager;
    [SerializeField] private InteractionUI interactionUI;

    private void Awake()
    {
        interactionManager.OnUpdateInteractablesList += InteractionManager_OnUpdateInteractablesList;
        interactionUI.Toggle(false);
    }

    private void Update()
    {
        if (interactionManager.GetCountOfInteractables() <= 0) interactionUI.Toggle(false);
        HandleInput();
    }

    private void InteractionManager_OnUpdateInteractablesList(string name, int activeIndex, int count, bool value)
    {
        if (interactionUI == null) return;
        interactionUI.UpdateUI(name, activeIndex, count, value);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<InteractableBase>(out InteractableBase interactable))
        {
            AddInteractable(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<InteractableBase>(out InteractableBase interactable))
        {
            RemoveInteractable(interactable);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactionManager.ActiveInteractable == null) return;
            interactionManager.ActiveInteractable.OnInteract(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            interactionManager.SwitchActiveInteractable();
        }
    }

    private void AddInteractable(InteractableBase interactable)
    {
        if (interactionManager.ActiveInteractable == null)
        {
            if (interactionUI != null) interactionUI.Toggle(true);
            interactionManager.ActiveInteractable = interactable;
        }
        interactionManager.AddInteractable(interactable);
    }

    private void RemoveInteractable(InteractableBase interactable)
    {
        interactionManager.RemoveInteractable(interactable);
    }
}
