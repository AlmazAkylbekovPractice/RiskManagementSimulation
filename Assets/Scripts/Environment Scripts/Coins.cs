using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coins : MonoBehaviour
{
    [SerializeField] public bool isChosen;
    [SerializeField] public bool isTaken;

    [SerializeField] private GameObject _redParticles;
    [SerializeField] private GameObject _greenParticles;

    public Rigidbody body;

    public string slimeType;

    public float value = 0;

    [SerializeField] public bool positiveDeal;
    [SerializeField] private float dealProbability;
    [SerializeField] private float _dealTimerMax;
    [SerializeField] private float _dealTimerMin;
    private float _dealTimer; // Timer for changing deal positive or negative

    [SerializeField] private float _timerMax;
    [SerializeField] private float _timerMin;
    private float _timer; // Timer for adding or subtracting a value from common balance

    [SerializeField] private float _riskProfit;

    private bool valueByTypeApplied = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        _timer = Random.Range(_timerMin, _timerMax);
        _dealTimer = Random.Range(_dealTimerMin, _dealTimerMax);
        

        RandomiseDeal();
    }

    private void RandomiseDeal()
    {
        var randomNum = Random.Range(0, 100);

        if (randomNum >= dealProbability)positiveDeal = false;
        else positiveDeal = true;
    }

    private void Update()
    {
        if (isTaken && isChosen)
        {
            LaunchDeal();
        }
    }

    public void LaunchDeal()
    {
        if (!valueByTypeApplied)
        {
            switch (slimeType)
            {
                case "Green":
                    value = GameManager.greenHouseBalance * 0.03f;
                    break;

                case "Yellow":
                    value = GameManager.yellowHouseBalance * 0.05f;
                    break;

                case "Red":
                    value = GameManager.redHouseBalance * 0.1f;
                    break;

                case "Violet":
                    value = GameManager.violetHouseBalance * 0.3f;
                    break;
            }

            valueByTypeApplied = true;
        }
        

        SelectDealType();
    }

    public void SelectDealType()
    {
        if (_dealTimer > 0)
        {
            _dealTimer -= Time.deltaTime;
        }
        else
        {
            var randomNum = Random.Range(0, 100);

            if (randomNum >= dealProbability)
            {
                positiveDeal = false;
            }
            else
            {
                positiveDeal = true;

                ApplyRiskedValue();
            }

            _dealTimer = Random.Range(_dealTimerMin, _dealTimerMax);
        }

        if (positiveDeal) Profit();
        else Loss();
    }

    public void Profit()
    {
        _greenParticles.SetActive(true); //Activating profit effect
        _redParticles.SetActive(false); //Disabling loss effect

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            var profit = value;

            GameManager.instance.AddBalance(profit,slimeType);

            _timer = Random.Range(_timerMin, _timerMax);
        }
    }

    public void Loss()
    {
        _greenParticles.SetActive(false); //Disabling profit effect
        _redParticles.SetActive(true); //Enabling loss effect

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            var loss = value;

            GameManager.instance.SubtractBalance(loss, slimeType);

            _riskProfit += Mathf.Abs(loss);

            _timer = Random.Range(_timerMin, _timerMax);
        }
    }

    private void ApplyRiskedValue()
    {
        GameManager.instance.AddBalance(_riskProfit, slimeType);
        _riskProfit = 0;
    }
    
    public void DeactivateCoins()
    {
        Destroy(gameObject);
    }
}
