using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private List<WaveConfig> waves;
    [SerializeField] private bool looping = false;

    IEnumerator Start() {
        do {
            yield return StartCoroutine(StartWaves());
        } while (looping);
    }

    IEnumerator StartWaves() {
        for (var i = 0; i < waves.Count; i++) {
            yield return StartCoroutine(StartWave(waves[i]));
        }
    }

    IEnumerator StartWave(WaveConfig waveConfig) {
        for (var i = 0; i < waveConfig.NumberOfEnemies; i++) {
            GameObject enemy = Instantiate(waveConfig.Enemy, transform.parent);
            enemy.GetComponent<EnemyPath>().setConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }
    }


// Update is called once per frame
    void Update()
    {
        
    }
}
