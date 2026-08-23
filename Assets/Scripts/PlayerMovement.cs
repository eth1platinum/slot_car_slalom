using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // ==========================================
    // POWER-UP UI
    // ==========================================

    [Header("Boost UI")]
    public Image boostDarkImage;
    public Image boostBrightImage;

    [Header("Slow UI")]
    public Image slowDarkImage;
    public Image slowBrightImage;

    [Header("Power-Up Recharge")]
    public float boostRechargeTime = 15f;
    public float slowRechargeTime = 15f;

    private float boostRechargeTimer = 0f;
    private float slowRechargeTimer = 0f;

    private bool boostReady = false;
    private bool slowReady = false;


    // ==========================================
    // MOVEMENT
    // ==========================================

    private float frameAccumulator = 0.0F;
    private const float step = 0.02F;
    private float maxMultiplier = 4f;

    private bool isSwitchingLane = false;

    PlayerPosition position = PlayerPosition.POSITION_CENTRE;


    // ==========================================
    // START
    // ==========================================

    private void Start()
    {
        // Both power-ups start empty and begin
        // recharging immediately.

        boostRechargeTimer = 0f;
        slowRechargeTimer = 0f;

        boostReady = false;
        slowReady = false;

        UpdateBoostUI();
        UpdateSlowUI();
    }


    // ==========================================
    // BOOST / SLOW EFFECT
    // ==========================================

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
            StartCoroutine(
                ApplyTemporaryBoost(
                    boostMultiplier,
                    boostDurationSecs
                )
            );
        }
    }


    // ==========================================
    // UPDATE
    // ==========================================

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
        // POWER-UP RECHARGING
        // ==========================================

        UpdateBoostRecharge();
        UpdateSlowRecharge();


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
        // W / UP ARROW
        // ==========================================

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryActivateBoost();
        }


        // ==========================================
        // REDUCE / SLOW
        // S / DOWN ARROW
        // ==========================================

        if (Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryActivateSlow();
        }
    }


    // ==========================================
    // BOOST RECHARGE
    // ==========================================

    private void UpdateBoostRecharge()
    {
        if (boostReady)
            return;

        boostRechargeTimer += Time.deltaTime;

        if (boostRechargeTimer >= boostRechargeTime)
        {
            boostRechargeTimer = boostRechargeTime;
            boostReady = true;
        }

        UpdateBoostUI();
    }


    // ==========================================
    // SLOW RECHARGE
    // ==========================================

    private void UpdateSlowRecharge()
    {
        if (slowReady)
            return;

        slowRechargeTimer += Time.deltaTime;

        if (slowRechargeTimer >= slowRechargeTime)
        {
            slowRechargeTimer = slowRechargeTime;
            slowReady = true;
        }

        UpdateSlowUI();
    }


    // ==========================================
    // TRY ACTIVATE BOOST
    // ==========================================

    private void TryActivateBoost()
    {
        // Can't use boost until fully charged.
        if (!boostReady)
        {
            Debug.Log("Boost is still recharging.");
            return;
        }

        // Preserve your existing multiplier restriction.
        if (boostMultiple * 2f > maxMultiplier)
        {
            Debug.Log("Boost cannot be activated because maximum multiplier would be exceeded.");
            return;
        }

        // Activate boost.
        StartCoroutine(
            ApplyTemporaryBoost(2f, 3f)
        );

        // Reset recharge.
        boostReady = false;
        boostRechargeTimer = 0f;

        UpdateBoostUI();

        Debug.Log("BOOST ACTIVATED!");
    }


    // ==========================================
    // TRY ACTIVATE SLOW
    // ==========================================

    private void TryActivateSlow()
    {
        // Can't use slow until fully charged.
        if (!slowReady)
        {
            Debug.Log("Slow is still recharging.");
            return;
        }

        // Preserve your existing multiplier restriction.
        if (boostMultiple * 0.5f < 1f)
        {
            Debug.Log("Slow cannot reduce the multiplier below 1.");
            return;
        }

        // Activate slow.
        StartCoroutine(
            ApplyTemporaryBoost(0.5f, 3f)
        );

        // Reset recharge.
        slowReady = false;
        slowRechargeTimer = 0f;

        UpdateSlowUI();

        Debug.Log("SLOW ACTIVATED!");
    }


    // ==========================================
    // BOOST UI
    // ==========================================

    private void UpdateBoostUI()
    {
        if (boostBrightImage == null)
            return;

        float progress =
            boostRechargeTimer / boostRechargeTime;

        boostBrightImage.fillAmount =
            Mathf.Clamp01(progress);
    }


    // ==========================================
    // SLOW UI
    // ==========================================

    private void UpdateSlowUI()
    {
        if (slowBrightImage == null)
            return;

        float progress =
            slowRechargeTimer / slowRechargeTime;

        slowBrightImage.fillAmount =
            Mathf.Clamp01(progress);
    }


    // ==========================================
    // LANE SWITCH
    // ==========================================

    private IEnumerator SwitchLane(float direction)
    {
        isSwitchingLane = true;

        Vector3 startPosition = transform.position;

        float targetX =
            startPosition.x +
            (playerMovement * direction);


        // -------------------------
        // 1. UP
        // -------------------------

        float elapsed = 0f;

        while (elapsed < hopTime)
        {
            elapsed += Time.deltaTime;

            float t =
                Mathf.Clamp01(elapsed / hopTime);

            transform.position = new Vector3(
                startPosition.x,
                Mathf.Lerp(
                    startPosition.y,
                    startPosition.y + hopHeight,
                    t
                ),
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

            float t =
                Mathf.Clamp01(elapsed / acrossTime);

            transform.position = new Vector3(
                Mathf.Lerp(
                    startPosition.x,
                    targetX,
                    t
                ),
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

            float t =
                Mathf.Clamp01(elapsed / hopTime);

            transform.position = new Vector3(
                targetX,
                Mathf.Lerp(
                    startPosition.y + hopHeight,
                    startPosition.y,
                    t
                ),
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
}