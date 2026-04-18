using System.Collections;
using UnityEngine;

public class SpeedMultiplier : MonoBehaviour
{
    public int boostMultiplier = 2;
    public float boostDurationSecs = 3F;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement movement = other.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            movement.ApplySpeedBoost(boostMultiplier, boostDurationSecs);
        }

    }
}
