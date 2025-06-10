using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInfo : MonoBehaviour
{
    [SerializeField] GameObject coinDisplay;
	
	void Awake() {
		
	}

    void Update()
    {
        coinDisplay.GetComponent<TMPro.TMP_Text>().text = "COINS: " + SaveLoadManager.Instance.Data.coinsCollected;
    }
}
