using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "spriteSheet", menuName = "SObjects/UI/Sprite Sheet")]
public class SpriteSheet : ScriptableObject
{
    [Serializable]
    public class Frame
    {
        public float time;
        public Sprite sprite;
    }

    public Frame[] frames;
    public bool loop;
}
