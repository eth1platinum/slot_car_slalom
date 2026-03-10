using UnityEngine;

public class MainGameLoader : MonoBehaviour
{
    public CarColourDatabase availableColours;
    public GameObject car;

    private MeshRenderer carRenderer;
    private Material carMaterial;
    private void Awake()
    {
        carRenderer = car.GetComponent<MeshRenderer>();
        Material[] mats = carRenderer.materials;
        carMaterial = mats[2]; // todo make this dynamic?
        ApplyCarColour();
    }

    public void ApplyCarColour()
    {
        string colourID = SaveLoadManager.Instance.Data.selectedCarColour;
        CarColourOption option = availableColours.colours.Find(c => c.colourID == colourID);
        carMaterial.SetColor("_Color", option.colourValue);
    }
}
