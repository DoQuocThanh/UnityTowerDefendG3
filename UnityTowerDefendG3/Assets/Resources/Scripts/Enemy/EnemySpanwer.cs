using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public EnemyController enemyToSpawn;
    public Transform spawnPoint;

    public float timePerSpawn;
    private float spawnCounter;
    public int totalSpawn = 10;
    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timePerSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalSpawn > 0)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = timePerSpawn;
                Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
                totalSpawn--;
            }
        }
    }
}
