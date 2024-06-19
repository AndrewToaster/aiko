using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string Name { get; }
    public bool IsInteractable { get; }
    public bool HoldToInteract { get; }
    public float HoldDuration { get; }

    public void OnInteract(GameObject character);
}
