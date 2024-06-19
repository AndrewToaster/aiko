using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] private Image itemImage; // no use for rn
    [SerializeField] private TextMeshProUGUI title; // no use for rn (added in case of new design)
    [SerializeField] private TextMeshProUGUI description;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        if (itemImage != null) itemImage.gameObject.SetActive(false);
        if (title != null) title.text = "";
        if (description != null) description.text = "";
    }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription)
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
        }
        if (title != null) title.text = itemName;
        if (description != null) description.text = itemDescription;
    }
}
