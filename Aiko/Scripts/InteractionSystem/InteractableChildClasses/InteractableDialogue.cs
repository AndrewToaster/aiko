using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogue : InteractableBase
{
    [SerializeField] List<string> dialogues;

    public override void OnInteract(GameObject character)
    {
        foreach (string dialogue in dialogues)
        {
            DialogueManager.Instance.AddText(dialogue);
        }
    }
}
