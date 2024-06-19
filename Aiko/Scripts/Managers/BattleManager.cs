using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BattleManager : ManagerBase<BattleManager>
{
    public BattleInstance battle;
    public BattleScreen screen;
    public BattleScript script;
    
    private Scene _previous;
    private GameObject[] _previousObjects;

    public bool StartBattle(BattleInstance instance)
    {
        if (battle != null) return false;

        _previousObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (var item in _previousObjects)
        {
            if (!item || item.TryGetComponent<ManagerTag>(out _)) continue;
            item.SetActive(false);
        }

        battle = instance;
        _previous = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Scenes.BATTLE, LoadSceneMode.Additive);

        GameStateManager.Instance.SetState(GameStateType.Battle);

        return true;
    }

    public bool ReturnFromBattle()
    {
        if (battle == null) return false;

        foreach (var item in _previousObjects)
        {
            if (!item || item.TryGetComponent<ManagerTag>(out _)) continue;
            item.SetActive(true);
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(Scenes.BATTLE));
        SceneManager.SetActiveScene(_previous);
        Destroy(script.gameObject);
        battle = null;

        GameStateManager.Instance.SetState(GameStateType.Game);

        return true;
    }
}
