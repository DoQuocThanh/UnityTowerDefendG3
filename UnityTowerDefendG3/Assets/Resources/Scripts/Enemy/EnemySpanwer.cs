using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public List<EnemyController> listEnemyToSpawn;
    public List<int> listNumberEnemy;
    public Transform spawnPoint;
    public float timePerSpawnMin;
    public float timePerSpawnMax;

    private float spawnCounter;
    private Base theBase;
    private float totalEnemyInSpawn;
    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = Random.Range(timePerSpawnMin, timePerSpawnMax);
        theBase = FindObjectOfType<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        if (listEnemyToSpawn.Count > 0 && theBase.currentHeath > 0)
        {

            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = Random.Range(timePerSpawnMin, timePerSpawnMax);
                int indexEnemy = Random.Range(0, listEnemyToSpawn.Count - 1);               
                EnemyController enemySpawn = listEnemyToSpawn[indexEnemy];                
                Instantiate(enemySpawn, spawnPoint.position, spawnPoint.rotation);
                listNumberEnemy[indexEnemy] -= 1;
                if (listNumberEnemy[indexEnemy] <= 0)
                {
                    listEnemyToSpawn.Remove(listEnemyToSpawn[indexEnemy]);
                    listNumberEnemy.Remove(listNumberEnemy[indexEnemy]);
                }
                totalEnemyInSpawn += 1;
                if (totalEnemyInSpawn >= 20) {
                    spawnCounter = 10;
                    totalEnemyInSpawn = 0;
                }

            }
        }
    }
}
