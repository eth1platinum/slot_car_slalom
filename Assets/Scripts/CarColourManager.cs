using System.Collections.Generic;
using UnityEngine;

public class CarColourManager : MonoBehaviour
{
    public CarColourDatabase availableColours;
    public CarColourDatabase unlockedColours;
    public GameObject car;

    private MeshRenderer carRenderer;
    private Material carMaterial;

    //private List<CarColourOption> unlockedColours;
    //private SaveData Data => SaveLoadManager.Instance.Data;

    private void Awake()
    {
        carRenderer = car.GetComponent<MeshRenderer>();
        Material[] mats = carRenderer.materials;
        carMaterial = mats[2]; // todo make this dynamic?
    }

    private void Start()
    {
        foreach (string id in SaveLoadManager.Instance.Data.unlockedCarColours)
        {
            // todo may not need this?
            CarColourOption option = availableColours.colours.Find(c => c.colourID == id);
            unlockedColours.colours.Add(option);
        }

        if (SaveLoadManager.Instance.Data.selectedCarColour != "") {
            ApplyColour(SaveLoadManager.Instance.Data.selectedCarColour);
        }
        else
        {
            SaveLoadManager.Instance.Data.selectedCarColour = "White";
        }
    }

    public void ApplyColour(string colourID)
    {
        CarColourOption option = unlockedColours.colours.Find(c => c.colourID == colourID);
        carMaterial.SetColor("_Color", option.colourValue);
        // todo make sure this only sets colours that are unlocked
    }
}
