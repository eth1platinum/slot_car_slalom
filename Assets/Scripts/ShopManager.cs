using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public CarColourDatabase availableColours;

    private void Start()
    {
        if (!SaveLoadManager.Instance.Data.unlockedCarColours.Contains("White"))
        {
            SaveLoadManager.Instance.Data.unlockedCarColours.Add("White");
        }
    }

    public void SelectColour(CarColourOption option)
    {
        if (IsUnlocked(option.colourID))
        {
            ApplyColour(option);
        }
        else
        {
            TryPurchase(option);
        }
    }

    public void SelectColourFromId(string colourID)
    {
        CarColourOption option = availableColours.colours.Find(c => c.colourID == colourID);
        SelectColour(option);
    }

    private void TryPurchase(CarColourOption option)
    {
        if (SaveLoadManager.Instance.Data.coinsCollected >= option.price)
        {
            SaveLoadManager.Instance.Data.coinsCollected -= option.price;

            UnlockColour(option.colourID);
            ApplyColour(option);

            SaveLoadManager.Instance.SaveGame();
        }
        else
        {
            Debug.Log("Not enough coins!");
            // todo make this do something else
        }
    }

    private void UnlockColour(string id)
    {
        if (!SaveLoadManager.Instance.Data.unlockedCarColours.Contains(id))
        {
            SaveLoadManager.Instance.Data.unlockedCarColours.Add(id);
        }
    }

    private bool IsUnlocked(string id)
    {
        return SaveLoadManager.Instance.Data.unlockedCarColours.Contains(id);
    }

    private void ApplyColour(CarColourOption option)
    {
        SaveLoadManager.Instance.Data.selectedCarColour = option.colourID;
        SaveLoadManager.Instance.SaveGame();
    }
}