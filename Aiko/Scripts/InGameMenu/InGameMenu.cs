using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject pouseMenuPanel;

    private void Awake() {
        pouseMenuPanel.SetActive(false);
    }

    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            EscapeMenu();
        }
    }

    private void EscapeMenu() {
        pouseMenuPanel.SetActive(true);
    }
    public void onBackToGameButton() {
        pouseMenuPanel.SetActive(false);
    }
    public void onSaveAndBackToMenu() {
        SaveData data = new SaveData(SaveSystem.Instance.player.GetComponent<InventoryController>().inventoryData);
        SaveSystem.Instance.Save(data);
        SceneManager.LoadScene("Menu");

    } 
}
