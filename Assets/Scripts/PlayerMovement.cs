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

    // Lane switch animation
    public float hopHeight = 0.5f;
    public float hopTime = 0.02f;
    public float acrossTime = 0.04f;

    public CoinManager coinManager;

    private float frameAccumulator = 0.0F;
    private const float step = 0.02F;
    private float maxMultiplier = 4f;

    private bool isSwitchingLane = false;

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

    private IEnumerator SwitchLane(float direction)
    {
        isSwitchingLane = true;

        Vector3 startPosition = transform.position;

        float targetX = startPosition.x + (playerMovement * direction);

        // -------------------------
        // 1. UP
        // -------------------------

        float elapsed = 0f;

        while (elapsed < hopTime)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / hopTime);

            transform.position = new Vector3(
                startPosition.x,
                Mathf.Lerp(startPosition.y, startPosition.y + hopHeight, t),
                transform.position.z
            );

            yield return null;
        }

        // Make sure we're exactly at the top
        transform.position = new Vector3(
            startPosition.x,
            startPosition.y + hopHeight,
            transform.position.z
        );


        // -------------------------
        // 2. ACROSS
        // -------------------------

        elapsed = 0f;

        while (elapsed < acrossTime)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / acrossTime);

            transform.position = new Vector3(
                Mathf.Lerp(startPosition.x, targetX, t),
                startPosition.y + hopHeight,
                transform.position.z
            );

            yield return null;
        }

        // Make sure we're exactly over the new lane
        transform.position = new Vector3(
            targetX,
            startPosition.y + hopHeight,
            transform.position.z
        );


        // -------------------------
        // 3. DOWN
        // -------------------------

        elapsed = 0f;

        while (elapsed < hopTime)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / hopTime);

            transform.position = new Vector3(
                targetX,
                Mathf.Lerp(startPosition.y + hopHeight, startPosition.y, t),
                transform.position.z
            );

            yield return null;
        }

        // Make sure we're exactly in the new lane
        transform.position = new Vector3(
            targetX,
            startPosition.y,
            transform.position.z
        );

        isSwitchingLane = false;
    }

    void Update()
    {
        // ==========================================
        // FORWARD MOVEMENT
        // ==========================================

        frameAccumulator += Time.deltaTime;

        while (frameAccumulator >= step)
        {
            float moveSpeed = step * playerSpeed * boostMultiple;

            transform.Translate(
                Vector3.forward * moveSpeed,
                Space.World
            );

            backWall.transform.Translate(
                Vector3.forward * moveSpeed,
                Space.World
            );

            frameAccumulator -= step;
        }


        // ==========================================
        // LEFT
        // ==========================================

        if (Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (position > PlayerPosition.POSITION_LEFT &&
                !isSwitchingLane)
            {
                StartCoroutine(SwitchLane(-1));

                position--;
            }
        }


        // ==========================================
        // RIGHT
        // ==========================================

        if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (position < PlayerPosition.POSITION_RIGHT &&
                !isSwitchingLane)
            {
                StartCoroutine(SwitchLane(1));

                position++;
            }
        }


        // ==========================================
        // BOOST
        // ==========================================

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (boostMultiple * 2f <= maxMultiplier)
            {
                StartCoroutine(
                    ApplyTemporaryBoost(2f, 3f)
                );
            }
        }


        // ==========================================
        // REDUCE
        // ==========================================

        if (Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (boostMultiple * 0.5f >= 1f)
            {
                StartCoroutine(
                    ApplyTemporaryBoost(0.5f, 3f)
                );
            }
        }
    }
}