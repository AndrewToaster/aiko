using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public TMP_Text text;
    public Button button;
    public OptionButtonData data;

    private void Awake()
    {
        button.onClick.AddListener(Click);
    }

    private void Click()
    {
        BattleManager.Instance.screen.optionList.FireOptionSelected(this);
    }

    public void Refresh()
    {
        text.SetText(data.text);
    }
}
