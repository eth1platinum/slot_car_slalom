using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int coinsCollected = 0;

    // Store unlocked colors by ID
    public List<string> unlockedCarColours = new List<string>();

    // Currently selected color
    public string selectedCarColour = "";

    // longest time spent in one run in seconds
    public float longestRunTime = 0f;
}
