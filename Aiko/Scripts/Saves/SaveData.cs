using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData {

    [SerializeField] private InventorySO inventoryData;

    public int playerHP;
    public float[] playerPosition;
    public List<InventoryItem> inventoryItems;

    public SaveData(InventorySO inventoryData) {
         this.inventoryData = inventoryData;

        playerPosition = new float[2];
        
        playerPosition[0] = SaveSystem.Instance.player.transform.position.x;
        playerPosition[1] = SaveSystem.Instance.player.transform.position.y;
        
        inventoryItems = new List<InventoryItem>();

        foreach (var item in inventoryData.inventoryItems) {
            inventoryItems.Add(item);
        }
        playerHP = SaveSystem.Instance.player.GetComponent<PlayerHealth>().health;
    }
}
