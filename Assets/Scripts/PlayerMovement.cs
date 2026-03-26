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
    public float playerSpeed = 6.0F;
    public float boostMultiple = 1.0F;
    public float playerMovement = 6.66F;
    public GameObject backWall;

    private float frameAccumulator = 0.0F; // helps to smooth forward movement and prevent jumpiness
    private const float step = 0.02F;

    PlayerPosition position = PlayerPosition.POSITION_CENTRE;

    public void ApplySpeedBoost(float boostMultiplier, float boostDurationSecs, float maxBoostMultiplier)
    {
        if (boostMultiple * boostMultiplier <= maxBoostMultiplier)
        {
            StartCoroutine(ApplyBoost(boostMultiplier, boostDurationSecs));
        }
    }

    private IEnumerator ApplyBoost(float boostMultiplier, float boostDurationSecs)
    {
        boostMultiple *= boostMultiplier;
        yield return new WaitForSeconds(boostDurationSecs);
        boostMultiple /= boostMultiplier;
    }

    void Update()
    {
        frameAccumulator += Time.deltaTime;

        while (frameAccumulator >= step)
        {
            float moveSpeed = step * playerSpeed * boostMultiple;
            transform.Translate(Vector3.forward * moveSpeed, Space.World);
            backWall.transform.Translate(Vector3.forward * moveSpeed, Space.World);
            frameAccumulator -= step;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && Input.anyKeyDown)
        { // if A or left is pressed then move left
            if (position > PlayerPosition.POSITION_LEFT)
            { // if exceeded left limit then don't move
                transform.Translate(Vector3.left * playerMovement);
                position--;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && Input.anyKeyDown)
        { // if D or right is pressed then move right
            if (position < PlayerPosition.POSITION_RIGHT)
            { // if exceeded right limit then don't move
                transform.Translate(Vector3.right * playerMovement);
                position++;
            }
        }
    }
}
