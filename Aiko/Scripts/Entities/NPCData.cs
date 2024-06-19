using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "SObjects/NPCData")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public Sprite sprite;
    public int health;
}
