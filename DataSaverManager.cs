using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * Singleton class that has public methods to be called from other classes (e.g. start, exit, pause etc).
 * 
 * LoadGame() first accesses Load() in FileDataHandler to obtain previous CircleData, 
 * then call LoadData(CircleData) in IDataSaver to update game.
 * 
 * SaveGame() first modifies CircleData using SaveData(ref CircleData) in IDataSaver, 
 * then call Save() in FileDataHandler to update json text.
 */

public class DataSaverManager : MonoBehaviour
{
    //get instance publicly, but modify privately
    public static DataSaverManager instance { get; private set; }

    private CircleData data;
    private List<IDataSaver> dataSaverObjects;

    private FileDataHandler dataHandler;
    private string fileName = "data.fun";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one instance of DataSaverManager");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataSaverObjects = FindAllDataSaverObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.data = new CircleData();
    }

    public void LoadGame()
    {
        this.data = dataHandler.Load();

        if (this.data == null)
        {
            Debug.Log("No data was saved, new game loading.");
            NewGame();
        }

        foreach (IDataSaver dataSaver in dataSaverObjects)
        { 
            dataSaver.LoadData(data);
        }
    }

    public void SaveGame()
    {
        //pass data to other scripts to update data
        //save data to a file using data handler
        foreach (IDataSaver dataSaver in dataSaverObjects)
        {
            dataSaver.SaveData(ref data);
        }

        dataHandler.Save(data);
    }

    private List<IDataSaver> FindAllDataSaverObjects()
    {
        IEnumerable<IDataSaver> dataSaverObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaver>();
        return new List<IDataSaver>(dataSaverObjects);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
