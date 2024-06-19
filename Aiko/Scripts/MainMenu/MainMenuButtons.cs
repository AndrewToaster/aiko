using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject saveMenu;
    public GameObject credits;

    public SaveSystem saveSystem;

    void Start () {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        saveMenu.SetActive(false);       
        credits.SetActive(false);           
    }
    public void onPlayButton() {
        mainMenu.SetActive(false);
        saveMenu.SetActive(true);
    }
    public void onQuitButton() {
        Debug.Log("quit");
        Application.Quit();
    }
    public void onOptionMenuButton() {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    public void onBackButton() {
        mainMenu.SetActive(true);
        saveMenu.SetActive(false);
        optionMenu.SetActive(false);

    }
    public void onLoadSave() {
        SceneManager.LoadScene("2DTest v2");
        SaveData idk = SaveSystem.Instance.Load();
        SaveSystem.Instance.setSaveData(idk);
    }
    public void onDeleteSave() {
        saveSystem.deleteSave();
    }
    public void onCredits() {
        optionMenu.SetActive(false);
        credits.SetActive(true);
    }
    public void onBackCredits() {
        optionMenu.SetActive(true);
        credits.SetActive(false);
    }
}
