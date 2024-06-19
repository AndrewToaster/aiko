using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatWalkieTalkieDialogue : CharacterStatModifierSO
{
    [SerializeField] private string[] messages;
    public override void AffectCharacter(GameObject character, float val)
    {
        string message = messages[Random.Range(0, messages.Length)];
        DialogueManager.Instance.AddText(message);
    }
}
