using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameStateManager : ManagerBase<GameStateManager>
{
    public GameStateType state;

    public event Action<GameStateType> OnChangeState;

    protected override void Awake()
    {
        base.Awake();
        state = GameStateType.Game;
    }

    public void SetState(GameStateType newState)
    {
        OnChangeState?.Invoke(newState);
        state = newState;
    }
}

public enum GameStateType
{
    Game,
    Battle
}
