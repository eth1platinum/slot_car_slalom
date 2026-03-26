using System.Collections;
using UnityEngine;

public class SpeedMultiplier : MonoBehaviour
{
    public float boostMultiplier = 2F;
    public float boostDurationSecs = 3F;
    public float maxMultiplier = 8F;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement movement = other.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            movement.ApplySpeedBoost(boostMultiplier, boostDurationSecs, maxMultiplier);
        }

    }
}
