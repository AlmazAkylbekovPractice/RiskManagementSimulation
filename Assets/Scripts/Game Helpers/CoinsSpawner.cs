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

    private void Awake()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < bags; i++)
        {
            GenerateRandomPoint();
            Instantiate(_coinsPrefab, spawnPoint, Quaternion.identity);
        }
    }

    private void GenerateRandomPoint()
    {
        spawnPoint.x = Random.Range(firstPoint.position.x, secondPoint.position.x);
        spawnPoint.z = Random.Range(firstPoint.position.z, secondPoint.position.z);
        spawnPoint.y = 2;
    }


}
