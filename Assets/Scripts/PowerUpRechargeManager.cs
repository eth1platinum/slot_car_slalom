using UnityEngine;
using UnityEngine.UI;

public class PowerUpRechargeManager : MonoBehaviour
{
    [Header("Boost")]
    [SerializeField] private Image boostDarkImage;
    [SerializeField] private Image boostBrightImage;
    [SerializeField] private float boostRechargeTime = 30.0f;

    [Header("Slow")]
    [SerializeField] private Image slowDarkImage;
    [SerializeField] private Image slowBrightImage;
    [SerializeField] private float slowRechargeTime = 30.0f;

    private float boostTimer;
    private float slowTimer;

    private bool boostReady;
    private bool slowReady;

    private void Start()
    {
        // Start both power-ups uncharged.
        boostTimer = 0f;
        slowTimer = 0f;

        boostReady = false;
        slowReady = false;

        UpdateBoostUI();
        UpdateSlowUI();
    }

    private void Update()
    {
        UpdateBoostRecharge();
        UpdateSlowRecharge();

        CheckInput();
    }

    private void UpdateBoostRecharge()
    {
        if (boostTimer < boostRechargeTime)
        {
            boostTimer += Time.deltaTime;

            if (boostTimer >= boostRechargeTime)
            {
                boostTimer = boostRechargeTime;
                boostReady = true;
            }

            UpdateBoostUI();
        }
    }

    private void UpdateSlowRecharge()
    {
        if (slowTimer < slowRechargeTime)
        {
            slowTimer += Time.deltaTime;

            if (slowTimer >= slowRechargeTime)
            {
                slowTimer = slowRechargeTime;
                slowReady = true;
            }

            UpdateSlowUI();
        }
    }

    private void CheckInput()
    {
        // UP ARROW - BOOST
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (boostReady)
            {
                ActivateBoost();
            }
        }

        // DOWN ARROW - SLOW
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (slowReady)
            {
                ActivateSlow();
            }
        }
    }

    private void ActivateBoost()
    {
        Debug.Log("BOOST ACTIVATED!");

        // Your actual boost code can go here,
        // or you can call another method/script.

        boostReady = false;
        boostTimer = 0f;

        UpdateBoostUI();
    }

    private void ActivateSlow()
    {
        Debug.Log("SLOW ACTIVATED!");

        // Your actual slow code can go here,
        // or you can call another method/script.

        slowReady = false;
        slowTimer = 0f;

        UpdateSlowUI();
    }

    private void UpdateBoostUI()
    {
        if (boostBrightImage != null)
        {
            float progress = boostTimer / boostRechargeTime;
            boostBrightImage.fillAmount = progress;
        }
    }

    private void UpdateSlowUI()
    {
        if (slowBrightImage != null)
        {
            float progress = slowTimer / slowRechargeTime;
            slowBrightImage.fillAmount = progress;
        }
    }

    // Optional public methods if other scripts need to activate
    // the power-ups rather than using the arrow keys directly.

    public void ActivateBoostExternally()
    {
        if (boostReady)
        {
            ActivateBoost();
        }
    }

    public void ActivateSlowExternally()
    {
        if (slowReady)
        {
            ActivateSlow();
        }
    }

    // Useful if you want to check availability from another script.

    public bool IsBoostReady()
    {
        return boostReady;
    }

    public bool IsSlowReady()
    {
        return slowReady;
    }
}