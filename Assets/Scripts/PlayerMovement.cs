using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerPosition
{
    POSITION_LEFT,
    POSITION_CENTRE,
    POSITION_RIGHT
}

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 3.0F;
    public float boostMultiple = 2f;
    public float playerMovement = 6.66F;
    public GameObject backWall;

    public CoinManager coinManager;

    private float frameAccumulator = 0.0F;
    private const float step = 0.02F;
    private float maxMultiplier = 4f;

    PlayerPosition position = PlayerPosition.POSITION_CENTRE;

    private IEnumerator ApplyTemporaryBoost(float multiplier, float duration)
    {
        // Apply boost
        boostMultiple *= multiplier;
        coinManager.coinMultiplier *= multiplier;

        yield return new WaitForSeconds(duration);

        // Revert boost
        boostMultiple /= multiplier;
        coinManager.coinMultiplier /= multiplier;
    }

    public void ApplySpeedBoost(float boostMultiplier, float boostDurationSecs)
    {
        if (boostMultiple * boostMultiplier <= maxMultiplier)
        {
            StartCoroutine(ApplyTemporaryBoost(boostMultiplier, boostDurationSecs));
        }
    }

    void Update()
    {
        // Forward movement
        frameAccumulator += Time.deltaTime;

        while (frameAccumulator >= step)
        {
            float moveSpeed = step * playerSpeed * boostMultiple;
            transform.Translate(Vector3.forward * moveSpeed, Space.World);
            backWall.transform.Translate(Vector3.forward * moveSpeed, Space.World);
            frameAccumulator -= step;
        }

        // LEFT
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (position > PlayerPosition.POSITION_LEFT)
            {
                transform.Translate(Vector3.left * playerMovement);
                position--;
            }
        }

        // RIGHT
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (position < PlayerPosition.POSITION_RIGHT)
            {
                transform.Translate(Vector3.right * playerMovement);
                position++;
            }
        }

        // BOOST (double for 3 seconds)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (boostMultiple * 2f <= maxMultiplier)
            {
                StartCoroutine(ApplyTemporaryBoost(2f, 3f));
            }
        }

        // REDUCE (half for 3 seconds)
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (boostMultiple * 0.5f >= 1f)
            {
                StartCoroutine(ApplyTemporaryBoost(0.5f, 3f));
            }
        }
    }
}