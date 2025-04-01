using UnityEngine;
using System.Collections;

public class BoatAISpawner : MonoBehaviour
{
    public GameObject boatPrefab; 
    public Transform spawnCenter; 
    public float spawnRadius = 50f; 
    public float spawnInterval = 5f; 
    public int maxBoats = 10; 

    private int currentBoatCount = 0;

    public void StartGame()
    {
        StartCoroutine(SpawnBoats());
    }

    IEnumerator SpawnBoats()
    {
        while (true)
        {
            if (currentBoatCount < maxBoats)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Instantiate(boatPrefab, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
                currentBoatCount++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        return new Vector3(spawnCenter.position.x + randomCircle.x, spawnCenter.position.y, spawnCenter.position.z + randomCircle.y);
    }
}
