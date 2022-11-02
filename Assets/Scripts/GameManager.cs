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

    public static GameManager instance { get; private set; }

    [SerializeField]
    public Text daysCountText;

    [SerializeField] public Text greenBalanceText;
    [SerializeField] public Text yellowBalanceText;


    private void Awake()
    {
        greenBalanceText.text = greenHouseBalance.ToString() + "$";
        yellowBalanceText.text = yellowHouseBalance.ToString() + "$";

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
        }
    }

    public void SubtractBalance(float inputBalance, string slimeType)
    {
        if (slimeType == "Green")
        {
            greenHouseBalance -= inputBalance;
            greenBalanceText.text = Mathf.Round(greenHouseBalance).ToString() + "$";
            greenBalanceText.color = Color.red;
        }
        else if (slimeType == "Yellow")
        {
            yellowHouseBalance -= inputBalance;
            yellowBalanceText.text = Mathf.Round(yellowHouseBalance).ToString() + "$";
            yellowBalanceText.color = Color.red;
        }
    }
}
