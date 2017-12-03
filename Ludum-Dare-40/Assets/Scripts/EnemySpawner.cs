using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private static EnemySpawner instance;

    public float secondsPerEnemy;
    public float radius;
    public float difficultyRate;
    public float minimumSecondsPerEnemy;
    public GameObject[] Enemies;
    public GameObject player;

    private float lastEnemySpawned = 0;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        while (true)
        {
            if (lastEnemySpawned + secondsPerEnemy < Time.time)
            {
                SpawnEnemy();
            }
            yield return null;
        }

    }

    public static void IncreaseDifficulty() {
        instance.secondsPerEnemy = Mathf.Clamp(instance.secondsPerEnemy - instance.difficultyRate, instance.minimumSecondsPerEnemy, instance.secondsPerEnemy);
    }

    void SpawnEnemy() {
        Vector2 position = (Vector2)player.transform.position + Random.insideUnitCircle.normalized * Random.Range(.5f, 1f) * radius;
        Instantiate(Enemies[Random.Range(0, Enemies.Length)], position, Quaternion.identity);
        lastEnemySpawned = Time.time;
    }

}
