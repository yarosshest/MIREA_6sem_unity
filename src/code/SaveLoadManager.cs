using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string savePath;

    public void SaveGame(GameData data)
    {
        savePath = Application.persistentDataPath.ToString() + "\\saveData.json";
        string json = JsonUtility.ToJson(data, true); // Convert GameData object to JSON string
        File.WriteAllText(savePath, json); // Write JSON string to file
        Debug.Log("Game saved to " + savePath);
    }

    public GameData LoadGame()
    {
        savePath = Application.persistentDataPath.ToString() + "\\saveData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath); // Read JSON string from file
            GameData data = JsonUtility.FromJson<GameData>(json); // Convert JSON string to GameData object
            Debug.Log("Game loaded from " + savePath);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found at " + savePath);
            return null;
        }
    }
}