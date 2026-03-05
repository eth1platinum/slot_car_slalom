using UnityEngine;
using System.Collections.Generic;

// todo tidy all of this

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<CarColourOption> availableColours;
    //[SerializeField] private CarColourManager carColourManager;

    private SaveData Data => SaveLoadManager.Instance.Data;

    private void Start()
    {
        if (!Data.unlockedCarColours.Contains("White"))
        { // todo change this to whatever is first in the list? make sure this actually works
            Debug.Log("Adding white to colour list");
            Data.unlockedCarColours.Add("White");
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
        SelectColour(availableColours[0]);
    }

    private void TryPurchase(CarColourOption option)
    {
        if (Data.coinsCollected >= option.price)
        {
            Data.coinsCollected -= option.price;

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
        if (!Data.unlockedCarColours.Contains(id))
        {
            Data.unlockedCarColours.Add(id);
        }
    }

    private bool IsUnlocked(string id)
    {
        return Data.unlockedCarColours.Contains(id);
    }

    private void ApplyColour(CarColourOption option)
    {
        //carColourManager.ApplyColour(option.colourValue);

        Data.selectedCarColour = option.colourID;
        SaveLoadManager.Instance.SaveGame();
    }
}