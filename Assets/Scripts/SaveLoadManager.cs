using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string saveFilePath;

    public void SaveGame(int coins, bool unlocked)
    {
		saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json"); // todo move this to one central location
		Debug.Log($"Save file location: {saveFilePath}");
		Debug.Log($"Saving game to: {saveFilePath}");
        SaveData data = new SaveData();
        data.coinsCollected = coins;
        data.collectiblesUnlocked = unlocked; // todo for future use

        string json = JsonUtility.ToJson(data, true); // pretty print for readability
        File.WriteAllText(saveFilePath, json);

        Debug.Log($"Game saved to: {saveFilePath}");
    }

    public SaveData LoadGame()
    {
		saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json"); // todo move this to one central location
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