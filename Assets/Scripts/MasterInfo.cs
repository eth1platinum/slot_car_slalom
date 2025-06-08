using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInfo : MonoBehaviour
{
    public static int coinCount = 0;
    [SerializeField] GameObject coinDisplay;
	
	void Awake() {
		SaveLoadManager manager = new SaveLoadManager();
		SaveData data = manager.LoadGame();
		coinCount = data.coinsCollected;
	}

    void Update()
    {
        coinDisplay.GetComponent<TMPro.TMP_Text>().text = "COINS: " + coinCount;
		saveGame(); // todo move this to when the run ends
    }
	
	public static void saveGame() { // todo make this non static?
		SaveLoadManager manager = new SaveLoadManager();
		manager.SaveGame(coinCount, false); // todo move all of this and change false here?
	}
}
