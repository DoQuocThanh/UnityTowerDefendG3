using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        public float timeBetweenSpawns;
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

    private IEnumerator SpawnWave(Wave wave)
    {
        foreach (var enemyUnit in wave.enemyUnits)
        {
            for (int i = 0; i < enemyUnit.quantity; i++)
            {
                Instantiate(enemyUnit.enemy, spawnPoint.position, spawnPoint.rotation);

                // Cập nhật số lượng quái vật còn lại ngay sau khi một quái vật mới xuất hiện
                UpdateRemainingEnemies(CountActiveEnemies());

                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }

        // Đợi cho đến khi tất cả quái vật đã bị tiêu diệt trước khi chuyển sang wave tiếp theo
        while (CountActiveEnemies() > 0)
        {
            yield return null;
        }
    }

    private void UpdateWaveInfo(int waveNumber)
    {
        waveText.text = "Wave: " + waveNumber;
    }

    private void UpdateRemainingEnemies(int remaining)
    {
        remainEnemyText.text = "Quái vật: " + remaining;
    }

    private int CountActiveEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
