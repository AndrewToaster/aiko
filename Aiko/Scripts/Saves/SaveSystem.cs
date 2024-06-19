using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : ManagerBase<SaveSystem>
{
    [SerializeField] private string dataFileName;
    [SerializeField] private string dataDirPath;
    public GameObject player;
    private InventorySO inventorySO;

    protected override void Awake()
    {
        base.Awake();
        dataDirPath = Application.persistentDataPath;
        player = GameObject.Find("Player");
    }

    public void Save(SaveData saveData) {

        string fullPath = Path.Combine(dataDirPath,dataFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); // create the directory if doesn't exist

        string dataToStore = JsonUtility.ToJson(saveData, true); // data to Json
        
        using (FileStream stream = new FileStream(fullPath, FileMode.Create)) { // write data to file
            using (StreamWriter writer = new StreamWriter(stream)) {
                writer.Write(dataToStore);
            }
        }
    }

    public SaveData Load() {

        string fullPath = Path.Combine(dataDirPath, dataFileName);
        SaveData loadedData = null;
        if (File.Exists(fullPath)) {
            string dataToLoad = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                using (StreamReader reader = new StreamReader(stream)) {
                    dataToLoad = reader.ReadToEnd();
                }
            }
            loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);  
        }
        return loadedData;
    }

    internal void deleteSave() {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath)) {
            Directory.Delete(Path.GetDirectoryName(fullPath), true);
        }
        else {
            Debug.LogWarning("not exist at path: " + fullPath);
        }
    }
    public void setSaveData(SaveData data) {
        player = GameObject.Find("Player");
        data.playerPosition[0] = player.transform.position.x;
        data.playerPosition[1] = player.transform.position.y;

        foreach (var item in data.inventoryItems) {
            inventorySO.inventoryItems.Add(item);
        }
        SaveSystem.Instance.player.GetComponent<PlayerHealth>().health = data.playerHP;
        
    }
}
