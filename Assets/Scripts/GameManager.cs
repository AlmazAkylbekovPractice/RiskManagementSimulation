using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int daysCount = 1;
    public static float greenHouseBalance = 100f;

    public static GameManager instance { get; private set; }

    [SerializeField]
    public Text daysCountText;

    [SerializeField]
    public Text greenBalanceText;


    private void Awake()
    {
        greenBalanceText.text = greenHouseBalance.ToString() + "$";
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

    public void AddGreenBalance(float inputBalance)
    {
        greenHouseBalance += inputBalance;
        greenBalanceText.text = Mathf.Round(greenHouseBalance).ToString() + "$";
        greenBalanceText.color = Color.green;
    }

    public void SubtractGreenBalance(float inputBalance)
    {
        greenHouseBalance -= inputBalance;
        greenBalanceText.text = Mathf.Round(greenHouseBalance).ToString() + "$";
        greenBalanceText.color = Color.red;
    }
}
