using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car/ColourDatabase")]
public class CarColourDatabase : ScriptableObject
{
    public List<CarColourOption> colours;
}
