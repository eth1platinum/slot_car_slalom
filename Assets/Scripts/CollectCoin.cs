using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] AudioSource coinFX;
    [SerializeField] int coinValue = 1;

    [SerializeField] CoinManager coinManager;

    void OnTriggerEnter(Collider other)
    {
        coinFX.Play();

        float finalValue = coinValue;

        finalValue *= coinManager.coinMultiplier;

        SaveLoadManager.Instance.Data.coinsCollected += Mathf.RoundToInt(finalValue);

        this.gameObject.SetActive(false);
    }
}