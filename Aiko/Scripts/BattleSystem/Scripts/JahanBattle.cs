using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class JahanBattle : BattleScript
{
    private State _nextState, _state;
    private bool _down, _next;

    private OptionButtonData _btnFight;
    private OptionButtonData _btnInteract;
    private OptionButtonData _btnInventory;
    private OptionButtonData _btnFlee;

    private enum State
    {
        None,
        Splash,
        PlayerTurn,
        EnemyTurn,
        Attack,
        Flee,
        Kill,
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        Next(State.Splash);

        _btnFight = OptionButtonData.Ctor("Fight", "BTN_FIGHT");
        _btnInteract = OptionButtonData.Ctor("Interact", "BTN_INTERACT");
        _btnInventory = OptionButtonData.Ctor("Inventory", "BTN_INVENTORY");
        _btnFlee = OptionButtonData.Ctor("Flee", "BTN_FLEE");

        _attack.OnHit += frac =>
        {
        };
    }

    protected override void OnOptionSelected(OptionButton data)
    {
        switch (data.data.id)
        {
            case "BTN_FIGHT":
                Next(State.Attack);
                break;

            case "BTN_FLEE":
                Next(State.Flee);
                break;

            default:
                break;
        }
    }

    protected override void OnAttackHit(float fraction)
    {
        float dist = 1 - 2 * Mathf.Abs(0.5f - fraction);
        print(dist);
        print(_screen.enemyHealth.Health);
        float rem = Mathf.Clamp01(_screen.enemyHealth.Health - dist / 2);
        print(rem);
        _screen.enemyHealth.SetHealthAmount(rem);
        Next(rem > 0 ? State.EnemyTurn : State.Kill);
    }

    private void Update()
    {
        bool ack = _down != (_down = Input.GetMouseButton(0)) && _down;
        bool enter, exit;
        if (_state != _nextState)
        {
            if (_next)
            {
                _next = false;
                exit = true;
                enter = false;
                print($"Exit: {_state}");
            }
            else
            {
                enter = true;
                exit = false;
                _state = _nextState; 
                print($"Enter: {_state}");
            }
            ack = false;
        }
        else
        {
            enter = exit = false;
        }

        switch (_state)
        {
            case State.Splash when enter:
                _screen.ShowDialogPanel();
                break;

            case State.Splash when ack:
                Next(State.PlayerTurn);
                break;

            case State.PlayerTurn when enter:
                _screen.ShowOptionPanel();
                _options.ClearOptions();
                _options.AddOptions(_btnFight, _btnInteract, _btnInventory, _btnFlee);
                _options.Refresh();
                break;

            case State.PlayerTurn when exit:
                _options.ClearOptions();
                break;

            case State.Attack when enter:
                _attack.Refresh();
                _screen.ShowAttackPanel();
                _attack.Attack(1);
                break;

            case State.EnemyTurn when enter:
                _screen.ShowDefendPanel();
                StartCoroutine(NextAfter(State.PlayerTurn, 2));
                break;

            case State.Flee when enter:
                _screen.ShowDialogPanel();
                _screen.dialogText.SetText("You fled...");
                break;

            case State.Kill when enter:
                _screen.ShowDialogPanel();
                _screen.dialogText.SetText("You murdered them :)");
                Destroy((GameObject)_battle.context);
                break;

            case State.Kill when ack:
            case State.Flee when ack:
                End();
                break;

            default:
                break;
        }
    }

    private IEnumerator NextAfter(State next, float time, bool runExit = true)
    {
        yield return new WaitForSeconds(time);
        Next(next, runExit);
    }

    private void Next(State next, bool runExit = true)
    {
        _nextState = next;
        _next = runExit;
    }
}
