using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactWithText;
    [SerializeField] private TextMeshProUGUI switchButtonText;
    [SerializeField] private TextMeshProUGUI switchText;

    public void SetInteractButtonText(string text)
    {
        interactButtonText.SetText(text);
    }

    public void SetInteractWithText(string text)
    {
        interactWithText.SetText(text);
    }

    public void SetSwitchButtonText(string text)
    {
        switchButtonText.SetText(text);
    }

    public void SetSwitchText(string text)
    {
        switchText.SetText(text);
    }

    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }

    public void UpdateUI(string name, int activeIndex, int count, bool value)
    {
        SetInteractButtonText("E");
        SetSwitchButtonText("Q");
        SetInteractWithText("Interact with " + name.ToString());
        SetSwitchText("Switch " + (activeIndex + 1) + "/" + count);
        Toggle(value);
    }
}
