using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float secondsPerEnemy;
    public float radius;
    public float maxEnemies;
    public float difficultyRate;
    public float minimumSecondsPerEnemy;
    public GameObject[] Enemies;
    public GameObject player;

    private float lastEnemySpawned = 0;

    // Use this for initialization
    void Start() {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        while (true)
        {
            if(lastEnemySpawned + secondsPerEnemy < Time.time)
            {
                SpawnEnemy();
            }
            yield return null;
        }

    }

    void SpawnEnemy() {
        Vector2 position = (Vector2)player.transform.position + Random.insideUnitCircle.normalized * Random.Range(.5f, 1f) * radius;
        Instantiate(Enemies[Random.Range(0, Enemies.Length)], position, Quaternion.identity);
        lastEnemySpawned = Time.time;
        secondsPerEnemy = Mathf.Clamp(secondsPerEnemy - difficultyRate, minimumSecondsPerEnemy, secondsPerEnemy);
    }

}
