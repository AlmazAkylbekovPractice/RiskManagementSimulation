using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int daysCount = 1;
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


    private void Awake()
    {
        greenBalanceText.text = greenHouseBalance.ToString() + "$";
        yellowBalanceText.text = yellowHouseBalance.ToString() + "$";
        redBalanceText.text = redHouseBalance.ToString() + "$";
        violetBalanceText.text = violetHouseBalance.ToString() + "$";

        daysCountText.text = daysCount.ToString();

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void AddBalance(float inputBalance, string slimeType)
    {
        if (slimeType == "Green")
        {
            greenHouseBalance += inputBalance;
            greenBalanceText.text = Mathf.Round(greenHouseBalance).ToString() + "$";
            greenBalanceText.color = Color.green;
        } else if (slimeType == "Yellow")
        {
            yellowHouseBalance += inputBalance;
            yellowBalanceText.text = Mathf.Round(yellowHouseBalance).ToString() + "$";
            yellowBalanceText.color = Color.yellow;
        } else if (slimeType == "Red")
        {
            redHouseBalance += inputBalance;
            redBalanceText.text = Mathf.Round(redHouseBalance).ToString() + "$";
            redBalanceText.color = Color.red;
        } else if (slimeType == "Violet")
        {
            violetHouseBalance += inputBalance;
            violetBalanceText.text = Mathf.Round(violetHouseBalance).ToString() + "$";
            violetBalanceText.color = violetColor;
        }
    }

    public void SubtractBalance(float inputBalance, string slimeType)
    {
        if (slimeType == "Green")
        {
            greenHouseBalance -= inputBalance;
            greenBalanceText.text = Mathf.Round(greenHouseBalance).ToString() + "$";
            greenBalanceText.color = Color.black;
        }
        else if (slimeType == "Yellow")
        {
            yellowHouseBalance -= inputBalance;
            yellowBalanceText.text = Mathf.Round(yellowHouseBalance).ToString() + "$";
            yellowBalanceText.color = Color.black;
        }
        else if (slimeType == "Red")
        {
            redHouseBalance -= inputBalance;
            redBalanceText.text = Mathf.Round(redHouseBalance).ToString() + "$";
            redBalanceText.color = Color.black;
        } else if (slimeType == "Violet")
        {
            violetHouseBalance -= inputBalance;
            violetBalanceText.text = Mathf.Round(violetHouseBalance).ToString() + "$";
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
}
