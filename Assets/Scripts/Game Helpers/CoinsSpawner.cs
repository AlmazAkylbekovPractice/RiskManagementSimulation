using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _coinsPrefab;

    [SerializeField] Transform firstPoint;
    [SerializeField] Transform secondPoint;

    [SerializeField] private float bags = 10;

    private Vector3 spawnPoint;


    public static CoinsSpawner instance;

    private void Awake()
    {
        GameManager.DayChanged += OnDayChanged;

        SpawnCoins();
    }


    public void SpawnCoins()
    {
        for (int i = 0; i < bags; i++)
        {
            GenerateRandomPoint();
            Instantiate(_coinsPrefab, spawnPoint, Quaternion.identity);
        }
    }

    public void GenerateRandomPoint()
    {
        spawnPoint.x = Random.Range(firstPoint.position.x, secondPoint.position.x);
        spawnPoint.z = Random.Range(firstPoint.position.z, secondPoint.position.z);
        spawnPoint.y = 2;
    }

    public void DeleteCoins()
    {
        GameObject[] bags = GameObject.FindGameObjectsWithTag("Bag");

        for (int i =0; i < bags.Length; i++)
        {
            if (bags[i].GetComponent<Coins>().isTaken == false)
                Destroy(bags[i]);
        }
    }

    void OnDayChanged()
    {
        DeleteCoins();
        SpawnCoins();
    }


}
