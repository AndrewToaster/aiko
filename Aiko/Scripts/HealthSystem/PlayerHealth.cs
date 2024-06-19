using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int health;

    public void AddHealth(int value)
    {
        health += value;
    }
}
