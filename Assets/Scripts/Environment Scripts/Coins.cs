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

    public int maxValue;
    public int minValue;

    [SerializeField] private bool positiveDeal;
    [SerializeField] private float dealProbability;
    [SerializeField] private float _dealTimerMax;
    [SerializeField] private float _dealTimerMin;
    private float _dealTimer; // Timer for changing deal positive or negative

    [SerializeField] private float _timerMax;
    [SerializeField] private float _timerMin;
    private float _timer; // Timer for adding or subtracting a value from common balance

    [SerializeField] private int _riskProfit;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        _timer = Random.Range(_timerMin, _timerMax);
        _dealTimer = Random.Range(_dealTimerMin, _dealTimerMax);
    }

    private void LateUpdate()
    {
        if (isTaken)
        {
            LaunchDeal();
        }
    }

    public void LaunchDeal()
    {
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
            dealProbability = Random.Range(0, 1);

            if (dealProbability == 0)
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
            var profit = Random.Range(minValue, maxValue);
            GameManager.instance.AddGreenBalance(profit);
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
            var loss = Random.Range(minValue, maxValue);
            GameManager.instance.SubtractGreenBalance(loss);

            _riskProfit += Mathf.Abs(loss);
            _timer = Random.Range(_timerMin, _timerMax);
        }
    }

    private void ApplyRiskedValue()
    {
        GameManager.instance.AddGreenBalance(_riskProfit);
        _riskProfit = 0;
    }
}
