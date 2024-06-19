using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(NPC))]
public class InteractibleBattle : InteractableBase
{
    [SerializeField] private NPCData _data;
    [SerializeField] private string _battleName;

    private void Awake()
    {
        _data = GetComponent<NPC>().data;
        Name = _data.npcName;
    }

    public override void OnInteract(GameObject character)
    {
        if (!BattleScript.SCRIPTS.TryGetValue(_battleName, out Type script))
        {
            Debug.LogWarning($"Failed to start battle: Script '{_battleName}' couldn't be found in {nameof(BattleScript.SCRIPTS)}!");
            return;
        }

        BattleManager.Instance.StartBattle(new BattleInstance(new BattleEnemyData()
        {
            battleName = _data.npcName,
            health = _data.health,
            sprite = _data.sprite
        }, $"<style=\"Important\">{_data.npcName}</style> blocks the way.", script, gameObject));
    }
}
