using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMessageUI : MonoBehaviour, IPointerClickHandler
{
    public event EventHandler OnButtonMessageClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonMessageClicked?.Invoke(this, EventArgs.Empty);
    }
}
