using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Handles saving and loading of game data across platforms (WebGL and Standalone).
/// </summary>
public static class SaveLoad
{
    static string fileName = "SaveFile.txt";             // Save file name.
    static string directoryName = "SpaceInvadUrsAttack"; // Save folder name.

    /// <summary>
    /// Saves the current game state to persistent storage.
    /// Uses PlayerPrefs for WebGL and binary serialization for standalone/editor platforms.
    /// </summary>
    /// <param name="saveObject">The object containing the game state to save.</param>
    public static void SaveState(SaveObject saveObject)
    {
#if UNITY_WEBGL
        string json = JsonUtility.ToJson(saveObject);                   // Convert save data to JSON.
        PlayerPrefs.SetString("SpaceInvadUrsAttack", json);             // Store JSON in PlayerPrefs.
        PlayerPrefs.Save();                                             // Force PlayerPrefs to save.
#elif UNITY_STANDALONE || UNITY_EDITOR
        if (!DirectoryExists())
        {
            Directory.CreateDirectory(GetSaveDirectory());              // Create directory if it doesn't exist.
        }

        BinaryFormatter bf = new BinaryFormatter();                     // Setup binary formatter.
        FileStream file = File.Create(GetSavePath());                   // Create or overwrite save file.
        bf.Serialize(file, saveObject);                                 // Serialize and write save data.
        file.Close();                                                   // Close the file stream.
#endif
    }

    /// <summary>
    /// Loads the game state from persistent storage.
    /// If no save is found, a new default state is created and saved.
    /// </summary>
    /// <returns>A <see cref="SaveObject"/> containing the loaded or default game state.</returns>
    public static SaveObject LoadState()
    {
        SaveObject saveObject = new SaveObject();                       // Create new default save object.

#if UNITY_WEBGL
        if (PlayerPrefs.HasKey("SpaceInvadUrsAttack"))
        {
            string json = PlayerPrefs.GetString("SpaceInvadUrsAttack"); // Retrieve JSON string.
            saveObject = JsonUtility.FromJson<SaveObject>(json);        // Deserialize JSON to object.
        }
        else
        {
            SaveState(saveObject);                                      // Save default if none exists.
        }
#elif UNITY_STANDALONE || UNITY_EDITOR
        if (SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();                 // Setup binary formatter.
                FileStream file = File.Open(GetSavePath(), FileMode.Open);  // Open save file.
                saveObject = (SaveObject)bf.Deserialize(file);              // Deserialize save data.
                file.Close();                                               // Close the file stream.
            }
            catch (SerializationException)
            {
                Debug.LogWarning("Failed to load save!");                   // Log load failure.
            }
        }
        else
        {
            SaveState(saveObject);                                          // Save default if none exists.
        }
#endif
        return saveObject;                                                  // Return loaded or default save.
    }

#if UNITY_STANDALONE || UNITY_EDITOR
    /// <summary>
    /// Checks if a save file already exists.
    /// </summary>
    /// <returns>True if the save file exists; otherwise, false.</returns>
    static bool SaveExists()
    {
        return File.Exists(GetSavePath());                                 // Check if save file exists.
    }

    /// <summary>
    /// Checks if the save directory exists.
    /// </summary>
    /// <returns>True if the directory exists; otherwise, false.</returns>
    static bool DirectoryExists()
    {
        return Directory.Exists(GetSaveDirectory());                       // Check if save directory exists.
    }

    /// <summary>
    /// Gets the full path of the save directory.
    /// </summary>
    /// <returns>The full directory path where the save file is stored.</returns>
    static string GetSaveDirectory()
    {
        return Path.Combine(Application.persistentDataPath, directoryName); // Combine base path and folder.
    }

    /// <summary>
    /// Gets the full path to the save file.
    /// </summary>
    /// <returns>The full path to the save file.</returns>
    static string GetSavePath()
    {
        return Path.Combine(GetSaveDirectory(), fileName);                   // Combine directory path and file name.
    }
#endif
}