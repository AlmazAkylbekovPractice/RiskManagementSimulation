using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static event Action DayChanged;

    public static int daysCount = 1;

    [SerializeField] public float dayTimer = 10f;
    private float _timer; 

    public static float greenHouseBalance = 100f;
    public static float yellowHouseBalance = 100f;
    public static float redHouseBalance = 100f;
    public static float violetHouseBalance = 100f;

    public Color violetColor = new Color(138, 0,255);

    public static GameManager instance { get; private set; }

    [SerializeField]
    public Text daysCountText;

    [SerializeField] public Text greenBalanceText;
    [SerializeField] public Text yellowBalanceText;
    [SerializeField] public Text redBalanceText;
    [SerializeField] public Text violetBalanceText;


    public int greenDeals = 0;
    public int yellowDeals = 0;
    public int redDeals = 0;
    public int violetDeals = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        LoadUI();
    }

    private void LoadUI()
    {
        greenBalanceText.text = greenHouseBalance.ToString() + "$";
        yellowBalanceText.text = yellowHouseBalance.ToString() + "$";
        redBalanceText.text = redHouseBalance.ToString() + "$";
        violetBalanceText.text = violetHouseBalance.ToString() + "$";
        daysCountText.text = daysCount.ToString();
    }

    private void LoadParameters()
    {

        //Set timer
        _timer = dayTimer;
    }

    private void Update()
    {
        DayTimer();
    }

    public void AddBalance(float inputBalance, string slimeType)
    {
        if (slimeType == "Green")
        {
            greenHouseBalance += inputBalance;
            greenBalanceText.text = Mathf.Round(greenHouseBalance).ToString() + " $";
            greenBalanceText.color = Color.green;
        } else if (slimeType == "Yellow")
        {
            yellowHouseBalance += inputBalance;
            yellowBalanceText.text = Mathf.Round(yellowHouseBalance).ToString() + " $";
            yellowBalanceText.color = Color.yellow;
        } else if (slimeType == "Red")
        {
            redHouseBalance += inputBalance;
            redBalanceText.text = Mathf.Round(redHouseBalance).ToString() + " $";
            redBalanceText.color = Color.red;
        } else if (slimeType == "Violet")
        {
            violetHouseBalance += inputBalance;
            violetBalanceText.text = Mathf.Round(violetHouseBalance).ToString() + " $";
            violetBalanceText.color = violetColor;
        }
    }

    public void SubtractBalance(float inputBalance, string slimeType)
    {
        if (slimeType == "Green")
        {
            greenHouseBalance -= inputBalance;
            greenBalanceText.text = Mathf.Round(greenHouseBalance).ToString() + " $";
            greenBalanceText.color = Color.black;
        }
        else if (slimeType == "Yellow")
        {
            yellowHouseBalance -= inputBalance;
            yellowBalanceText.text = Mathf.Round(yellowHouseBalance).ToString() + " $";
            yellowBalanceText.color = Color.black;
        }
        else if (slimeType == "Red")
        {
            redHouseBalance -= inputBalance;
            redBalanceText.text = Mathf.Round(redHouseBalance).ToString() + " $";
            redBalanceText.color = Color.black;
        } else if (slimeType == "Violet")
        {
            violetHouseBalance -= inputBalance;
            violetBalanceText.text = Mathf.Round(violetHouseBalance).ToString() + " $";
            violetBalanceText.color = Color.black;
        }
    }

    public static float GetSlimeType(string inputType)
    {
        float balance = 0;

        if (inputType == "Green")
        {
            balance = greenHouseBalance;
        }
        else if (inputType == "Yellow")
        {
            balance = yellowHouseBalance;
        }
        else if (inputType == "Red")
        {
            balance = redHouseBalance;
        } else if (inputType == "Violet")
        {
            balance = violetHouseBalance;
        }
        return balance;
    }

    public void CountDeals(string inputType)
    {
        if (inputType == "Green")
        {
            greenDeals++;
        }
        else if (inputType == "Yellow")
        {
            yellowDeals++;
        }
        else if (inputType == "Red")
        {
            redDeals++;
        }
        else if (inputType == "Violet")
        {
            violetDeals++;
        }
    }

    public void DayTimer()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        } else
        {
            daysCount++;
            daysCountText.text = daysCount.ToString();

            DayChanged?.Invoke();

            _timer = dayTimer ;
        }
    }
}
