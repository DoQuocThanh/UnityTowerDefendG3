using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpanwerEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField] private int baseEmemies = 8;
    [SerializeField] private float eneminiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("Events")]

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    public static int enemiesAlies;
    public  int  showEnemiesAlies;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
      
    }

    void Update()
    {
        showEnemiesAlies = enemiesAlies;
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eneminiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlies++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlies == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void Start()
    {
        StartWave();
    } 


    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = baseEmemies;
    }
    // Update is called once per frame

   

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
    }
    private void SpawnEnemy()
    {
        GameObject prefabsToSpawn = enemyPrefabs[0];
        Instantiate(prefabsToSpawn, Levelmanage.main.startPoint.position, Quaternion.identity);
    }
}
