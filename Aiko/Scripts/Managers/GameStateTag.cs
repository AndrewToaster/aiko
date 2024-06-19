using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class GameStateTag : MonoBehaviour
{
    public GameStateType GameState { get; set; }

    private void Awake()
    {
        GameStateManager.Instance.OnChangeState += OnChangeState;
    }

    private void OnChangeState(GameStateType state)
    {
        gameObject.SetActive(state == GameState);
    }
}
