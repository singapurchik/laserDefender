using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaweConfig> waweConfigs;
    int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping == true);
    }

    IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waweConfigs.Count; waveIndex++)
        {
            var currentWave = waweConfigs[waveIndex];
            yield return StartCoroutine(SpawAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawAllEnemiesInWave(WaweConfig waweConfig)
    {
        for(int enemyCount = 0; enemyCount < waweConfig.GetNumbetOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                        waweConfig.GetEnemyPrefab(),
                        waweConfig.GetWaypoints()[0].transform.position,
                        Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waweConfig);
            yield return new WaitForSeconds(waweConfig.GetTimeBetweenSpawns());
        }
    }
}
