using UnityEngine;
using TMPro;
using System;
public class GameOverManager : MonoBehaviour
{
    public TMP_Text LongestRunLabel;
    public TMP_Text TotalCoinsLabel;

    void Start()
    {
        if (SceneLoader.brokePersonalBest)
        {
            LongestRunLabel.gameObject.SetActive(true);
            TimeSpan time = TimeSpan.FromSeconds(SaveLoadManager.Instance.Data.longestRunTime);
            string longestTimeString = string.Format(
                "{0:00}:{1:00}:{2:00}",
                time.Hours,
                time.Minutes,
                time.Seconds
            );

            LongestRunLabel.text = "New Longest Run! " + longestTimeString;
        }
        else
        {
            LongestRunLabel.gameObject.SetActive(false);
        }

        TotalCoinsLabel.text = "New Coin Total: " + SaveLoadManager.Instance.Data.coinsCollected;
    }
}