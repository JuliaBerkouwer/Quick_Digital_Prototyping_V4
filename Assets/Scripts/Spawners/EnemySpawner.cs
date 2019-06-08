using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    public float timeToIncreaseDiffeculty = 30f;
    public float maxDifficulty = 1f;
    public float maxSpawnRateInSeconds = 5f;

    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, timeToIncreaseDiffeculty);
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds); // hoe groter de maxSpawnRateInSecondsd des te kleiner de kans dat er eem ememy iedere seconde spawnt
        }
        else
        {
            spawnInSeconds = 1f;
        }
        Invoke("SpawnEnemy", spawnInSeconds);

    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > maxDifficulty)  // Als de max spawn rate groter is dan de max difficulty dan loopt die af naar beneden
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == maxDifficulty)
            CancelInvoke("IncreaseSpawnRate");
    }

}
