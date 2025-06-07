using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 6f; // ±âº» Easy

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        float xPos = Random.Range(-8f, 8f);
        Vector3 spawnPos = new Vector3(xPos, 6f, 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    public void SetDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy": spawnInterval = 10f; break;
            case "Normal": spawnInterval = 6f; break;
            case "Hard": spawnInterval = 3f; break;
            default: spawnInterval = 10f; break;
        }
    }
}
