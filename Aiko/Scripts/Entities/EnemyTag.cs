using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyTag : MonoBehaviour, IEnemy
{
    public Sprite sprite;
    public string enemyName;
    public int maxHealth;

    public Sprite GetBattleSprite()
    {
        return sprite;
    }

    public string GetBattleName()
    {
        return enemyName;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
