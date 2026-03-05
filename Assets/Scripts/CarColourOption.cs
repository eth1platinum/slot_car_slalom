using UnityEngine;

[System.Serializable]
public class CarColourOption
{
    public string colourID;        // Unique ID (e.g. "Red", "Blue")
    public Color colourValue;      // The actual Unity color
    public int price;             // Cost in coins
}
