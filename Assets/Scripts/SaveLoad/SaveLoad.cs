using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    private static string fileName = "SaveData.txt";
    private static string directoryName = "SaveData";

    public static void SaveState(SaveObject saveObject)
    {
        if (!DirectoryExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directoryName);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetSavePath());
        bf.Serialize(file, saveObject);
        file.Close();

    }

    public static SaveObject LoadState()
    {
        SaveObject saveObject = new SaveObject();

        if (SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetSavePath(), FileMode.Open);
                saveObject = (SaveObject)bf.Deserialize(file);
                file.Close();
            }
            catch (SerializationException)
            {
                Debug.LogWarning("Failed to load save!");
            }
        }

        return saveObject;
    }

    private static bool SaveExists()
    {
        return File.Exists(GetSavePath());
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directoryName);
    }

    private static string GetSavePath()
    {
        return Application.persistentDataPath + "/" + directoryName + "/" + fileName;
    }
}