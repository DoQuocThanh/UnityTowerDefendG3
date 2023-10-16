using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static WaveSpawner;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyUnit
    {
        public GameObject enemy;
        public int quantity;
    }

    [System.Serializable]
    public class Wave
    {
        public List<EnemyUnit> enemyUnits;
        public float timeBetweenSpawnsMin;
        public float timeBetweenSpawnsMax;
    }

    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves;
    public TextMeshProUGUI waveText;        // Text để hiển thị sóng
    public TextMeshProUGUI remainEnemyText;   // Text mới để hiển thị số lượng quái vật còn lại

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (; currentWave < waves.Length; currentWave++)
        {
            UpdateWaveInfo(currentWave + 1);
            yield return StartCoroutine(SpawnWave(waves[currentWave]));
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void Update()
    {
        UpdateRemainingEnemies();
    }

    private IEnumerator SpawnWave(Wave wave)
    {

        int indexEnemy = 0;
        float timeRandom = 0;
        while (wave.enemyUnits.Sum(x => x.quantity) > 0) {
            indexEnemy = Random.Range(0, wave.enemyUnits.Count());
            timeRandom = Random.Range(wave.timeBetweenSpawnsMin, wave.timeBetweenSpawnsMax);
            Instantiate(wave.enemyUnits[indexEnemy].enemy, spawnPoint.position, spawnPoint.rotation);
            wave.enemyUnits[indexEnemy].quantity -= 1;
            if(wave.enemyUnits[indexEnemy].quantity == 0)
            wave.enemyUnits.Remove(wave.enemyUnits[indexEnemy]);

            yield return new WaitForSeconds(timeRandom);
        }


        //foreach (var enemyUnit in wave.enemyUnits)
        //{
        //    for (int i = 0; i < enemyUnit.quantity; i++)
        //    {
        //        Instantiate(enemyUnit.enemy, spawnPoint.position, spawnPoint.rotation);

        //        // Cập nhật số lượng quái vật còn lại ngay sau khi một quái vật mới xuất hiện
        //        //;

               
        //    }
        //}

        // Đợi cho đến khi tất cả quái vật đã bị tiêu diệt trước khi chuyển sang wave tiếp theo
        //while (CountActiveEnemies() > 0)
        //{
        //    yield return null;
        //}
    }

    private void UpdateWaveInfo(int waveNumber)
    {
        waveText.text = "Wave: " + waveNumber;
    }

    public  void UpdateRemainingEnemies()
    {
        remainEnemyText.text = "Quái vật: " + CountActiveEnemies();
    }

    public  int CountActiveEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
