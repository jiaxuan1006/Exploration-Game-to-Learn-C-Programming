using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // The coin prefab to spawn
    public int numberOfCoins = 20; // Number of coins to spawn
    public Vector3 spawnArea; // The area within which coins will be spawned

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(0, spawnArea.y),
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            ) + transform.position; // Add the position of the CoinSpawner
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}
