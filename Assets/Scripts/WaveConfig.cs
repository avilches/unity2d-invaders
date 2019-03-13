using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "WaveConfig", menuName="Wave config")]
public class WaveConfig : ScriptableObject {
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject path;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float moveSpeed = 2f;

    public GameObject Enemy => enemy;
    
    public GameObject Path => path;

    public List<Transform> getWaypoints() {
        return Tools.listTransformChildren(path);
    }

    public float TimeBetweenSpawns => timeBetweenSpawns;

    public float SpawnRandomFactor => spawnRandomFactor;

    public int NumberOfEnemies => numberOfEnemies;

    public float MoveSpeed => moveSpeed;
}