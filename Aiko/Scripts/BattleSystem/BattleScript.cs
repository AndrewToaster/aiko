using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleScript : MonoBehaviour
{
    public static readonly Dictionary<string, Type> SCRIPTS = new()
    {
        ["JAHAN"] = typeof(JahanBattle)
    };

    protected bool _started;
    protected OptionList _options;
    protected AttackStrip _attack;
    protected BattleInstance _battle;
    protected BattleScreen _screen;
    protected BattleManager _manager;
    protected BattleEnemyData _enemy;

    protected virtual void Awake()
    {
        enabled = false;
        _manager = BattleManager.Instance;
        _screen = _manager.screen;
        _battle = _manager.battle;
        _options = _screen.optionList;
        _attack = _screen.attack;
        _enemy = _battle.enemy;
    }

    protected virtual void OnEnable()
    {
        _options.OnOptionSelected += OnOptionSelected;
        _attack.OnHit += OnAttackHit;
    }

    protected virtual void OnDisable()
    {
        _options.OnOptionSelected -= OnOptionSelected;
        _attack.OnHit -= OnAttackHit;
    }

    public void Begin(bool enable)
    {
        if (_started) return;

        _screen.HidePanels();

        _started = true;
        OnBegin();
        enabled = enable;
    }

    protected virtual void OnBegin()
    {
        BattleManager.Instance.screen.dialogText.text = BattleManager.Instance.battle.splashText;
    }

    public void End()
    {
        if (!_started) return;
        _started = false;
        OnEnd();
        enabled = false;
    }

    protected virtual void OnEnd()
    {
        BattleManager.Instance.ReturnFromBattle();
    }

    protected virtual void OnOptionSelected(OptionButton data)
    {
    }

    protected virtual void OnAttackHit(float fraction)
    {
    }
}
