using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _foodPrefab;

    [SerializeField] Transform firstPoint;
    [SerializeField] Transform secondPoint;

    [SerializeField] private float foodNum = 10;

    private Vector3 spawnPoint;

    private void Awake()
    {
        SpawnFood();
    }

    private void SpawnFood()
    {
        for (int i = 0; i < foodNum; i++)
        {
            GenerateRandomPoint();
            Instantiate(_foodPrefab, spawnPoint, Quaternion.identity);
        }
    }

    private void GenerateRandomPoint()
    {
        spawnPoint.x = Random.Range(firstPoint.position.x, secondPoint.position.x);
        spawnPoint.z = Random.Range(firstPoint.position.z, secondPoint.position.z);
        spawnPoint.y = 2;
    }


}
