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
#if UNITY_WEBGL
        string json = JsonUtility.ToJson(saveObject);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();

#elif UNITY_STANDALONE || UNITY_EDITOR
        if (!DirectoryExists())
        {
            Directory.CreateDirectory(GetSaveDirectory());
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetSavePath());
        bf.Serialize(file, saveObject);
        file.Close();
#endif
    }

    public static SaveObject LoadState()
    {
        SaveObject saveObject = new SaveObject();

#if UNITY_WEBGL
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            saveObject = JsonUtility.FromJson<SaveObject>(json);
        }
        else
        {
            SaveState(saveObject);
        }

#elif UNITY_STANDALONE || UNITY_EDITOR
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
        else
        {
            SaveState(saveObject);
        }
#endif

        return saveObject;
    }

#if UNITY_STANDALONE || UNITY_EDITOR
    private static bool SaveExists()
    {
        return File.Exists(GetSavePath());
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(GetSaveDirectory());
    }

    private static string GetSaveDirectory()
    {
        return Path.Combine(Application.persistentDataPath, directoryName);
    }

    private static string GetSavePath()
    {
        return Path.Combine(GetSaveDirectory(), fileName);
    }
#endif
}