using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count; // number of enemies you want to spawn
        public float delay; // the rate you want to spawn the enemies in, basically the spawn delay in seconds for each enemy
    }

    public Wave[] waves;
    private int nextWave = 0;


    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;  // the time between each wave takes to spawn
    private float waveCountDown; // shows which number of wave the spawner is at

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        //rate = Random.Range(1f, 3f);
        if (spawnPoints.Length == 0)
        {

        }
        waveCountDown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) // Add on things here if you've completed all waves
        {
            //ScoreManager.Instance.ResetSceneUI();
            nextWave = 0;
        }
        else
        {
            nextWave++;
            //ScoreManager.Instance.WaveCounter(nextWave);
        }
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("EnemyShipTag") == null)
            {
                return false;
            }

        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(_wave.delay);

        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
