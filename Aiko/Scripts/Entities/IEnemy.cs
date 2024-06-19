using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IEnemy
{
    public Sprite GetBattleSprite();
    public string GetBattleName();
    public int GetMaxHealth();
}
