using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager
{
    private const string saveFileName = "saveData.dat";

    private static GameManager instance;

    public int PlayerMoney { get; set; }


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }

    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFileName);

        SaveData saveData = new SaveData();
        saveData.PlayerMoney = PlayerMoney;

        binaryFormatter.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + saveFileName, FileMode.Open);

            SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);

            file.Close();


            PlayerMoney = saveData.PlayerMoney;
        }
    }
}

[Serializable]
class SaveData
{
    public int PlayerMoney { get; set; }

}
