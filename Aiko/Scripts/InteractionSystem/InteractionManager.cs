using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public List<InteractableBase> interactables = new List<InteractableBase>();
    private InteractableBase activeInteractable;
    private int activeIndex = 0;

    public event Action<String, int, int, bool> OnUpdateInteractablesList;

    public InteractableBase ActiveInteractable
    {
        get { return activeInteractable; }
        set { activeInteractable = value; } 
    }

    public void AddInteractable(InteractableBase interactable)
    {
        if (interactables.Contains(interactable)) return;
        interactables.Add(interactable);
        UpdateInteractables();
    }

    public void RemoveInteractable(InteractableBase interactable)
    {
        CheckIndex(interactables.IndexOf(interactable));
        interactables.Remove(interactable);
        if (interactables.Count > 0) activeInteractable = interactables[activeIndex];
        UpdateInteractables();
    }

    private void CheckIndex(int index)
    {
        if (index <= activeIndex)
        {
            activeIndex--;
            if (activeIndex < 0)
            {
                activeIndex = 0;
            }
        }
    }

    private void UpdateInteractables()
    {
        bool isInteractablesNotEmpty = true;
        if (interactables.Count == 0)
        {
            activeInteractable = null;
            isInteractablesNotEmpty = false;
        }
        string name = (activeInteractable == null) ? string.Empty : activeInteractable.Name;
        OnUpdateInteractablesList?.Invoke(name, activeIndex, interactables.Count, isInteractablesNotEmpty);
    }

    public void SwitchActiveInteractable()
    {
        activeIndex++;
        if (activeIndex > interactables.Count - 1)
        {
            activeIndex = 0;
        }
        if (interactables.Count > 0) activeInteractable = interactables[activeIndex];
        UpdateInteractables();
    }

    public List<InteractableBase> getInteractables()
    {
        return interactables;
    }

    public int getActiveIndexOfInteractable()
    {
        return activeIndex;
    }

    public int GetCountOfInteractables()
    {
        return interactables.Count;
    }
}
