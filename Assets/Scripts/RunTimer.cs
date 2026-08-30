using System;
using TMPro;
using UnityEngine;

public class RunTimer : MonoBehaviour
{
    public static RunTimer Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;

    private float startTime;
    private float currentRunTime;
    private bool isRunning;

    public float CurrentRunTime => currentRunTime;
    public bool IsRunning => isRunning;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (!isRunning)
            return;

        currentRunTime = Time.unscaledTime - startTime;

        UpdateTimerUI();
    }

    public void StartRun()
    {
        startTime = Time.unscaledTime;
        currentRunTime = 0f;
        isRunning = true;

        UpdateTimerUI();
    }

    public void StopRun()
    {
        if (!isRunning)
            return;

        currentRunTime = Time.unscaledTime - startTime;
        isRunning = false;

        UpdateTimerUI();

        CheckPersonalBest();
    }

    private void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        TimeSpan time = TimeSpan.FromSeconds(currentRunTime);

        timerText.text = string.Format(
            "{0:00}:{1:00}:{2:00}",
            time.Hours,
            time.Minutes,
            time.Seconds
        );
    }

    private void CheckPersonalBest()
    {
        if (currentRunTime > SaveLoadManager.Instance.Data.longestRunTime)
        {
            SaveLoadManager.Instance.Data.longestRunTime = currentRunTime;

            Debug.Log($"New personal best! {FormatTime(currentRunTime)}"); // todo add this to UI
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);

        return string.Format(
            "{0:00}:{1:00}:{2:00}",
            time.Hours,
            time.Minutes,
            time.Seconds
        );
    }
}