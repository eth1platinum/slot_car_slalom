using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    public SaveData Data { get; private set; }

    private string saveFilePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        Data = LoadGame();
    }

    public void SaveGame()
    {            
		Debug.Log($"Save file location: {saveFilePath}");
		Debug.Log($"Saving game to: {saveFilePath}");

        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log($"Game saved to: {saveFilePath}");
    }

    public SaveData LoadGame()
    {
		Debug.Log($"Save file location: {saveFilePath}");
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded successfully.");
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found. Returning default data.");
            return new SaveData(); // default values (coins=0, unlocked=false)
        }
    }
}