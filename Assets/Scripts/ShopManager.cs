using UnityEngine;
using System.Collections.Generic;

// todo tidy all of this

public class ShopManager : MonoBehaviour
{
    //[SerializeField] private List<CarColourOption> availableColours;
    public CarColourDatabase availableColours;
    //[SerializeField] private CarColourManager carColourManager;

    //private SaveData Data => SaveLoadManager.Instance.Data;

    private void Start()
    {
        if (!SaveLoadManager.Instance.Data.unlockedCarColours.Contains("White"))
        { // todo change this to whatever is first in the list? make sure this actually works
            Debug.Log("Adding white to colour list");
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

    public void testFunc() // todo change all of this
    {
        SelectColour(availableColours.colours[1]);
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
        //carColourManager.ApplyColour(option.colourValue);

        SaveLoadManager.Instance.Data.selectedCarColour = option.colourID;
        SaveLoadManager.Instance.SaveGame();
    }
}