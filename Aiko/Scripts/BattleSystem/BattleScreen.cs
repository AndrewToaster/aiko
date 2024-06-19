using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BattleScreen : MonoBehaviour
{
    private void Awake()
    {
        BattleManager.Instance.screen = this;
        StartCoroutine(DelayStart());
    }

    public void HidePanels()
    {
        actionPanel.gameObject.SetActive(false);
        dialogPanel.gameObject.SetActive(false);
        optionPanel.gameObject.SetActive(false);
        attackPanel.gameObject.SetActive(false);
        defendPanel.gameObject.SetActive(false);
    }

    public void ShowActionPanel(bool hideOthers = true)
    {
        if (hideOthers) HidePanels();
        actionPanel.gameObject.SetActive(true);
    }

    public void ShowDialogPanel(bool hideOthers = true)
    {
        if (hideOthers) HidePanels();
        dialogPanel.gameObject.SetActive(true);
    }

    public void ShowOptionPanel(bool hideOthers = true)
    {
        if (hideOthers) HidePanels();
        actionPanel.gameObject.SetActive(true);
        optionPanel.gameObject.SetActive(true);
    }

    public void ShowAttackPanel(bool hideOthers = true)
    {
        if (hideOthers) HidePanels();
        attackPanel.gameObject.SetActive(true);
    }

    public void ShowDefendPanel(bool hideOthers = true)
    {
        if (hideOthers) HidePanels();
        defendPanel.gameObject.SetActive(true);
    }

    private IEnumerator DelayStart()
    {
        BattleManager.Instance.script = (BattleScript)new GameObject("BattleScriptEmpty")
            .AddComponent(BattleManager.Instance.battle.scriptType);
        BattleManager.Instance.script.Begin(false);
        yield return null;
        //yield return new WaitForSeconds(2);
        BattleManager.Instance.script.enabled = true;
    }

    public CanvasGroup dialogPanel;
    public CanvasGroup optionPanel;
    public CanvasGroup actionPanel;
    public CanvasGroup attackPanel;
    public CanvasGroup healthPanel;
    public CanvasGroup enemyHealthPanel;
    public CanvasGroup defendPanel;
    public TMP_Text dialogText;
    public Image enemySprite;
    public OptionList optionList;
    public AttackStrip attack;
    public HealthStrip health;
    public EnemyHealthStrip enemyHealth;
    public DefendScript defend;
}
