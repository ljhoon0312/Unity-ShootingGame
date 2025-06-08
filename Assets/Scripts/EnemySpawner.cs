using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;

    private float timer;

    void Start()
    {
        SetDifficulty(GameSettings.difficulty);
        Debug.Log($"[INIT] Difficulty = {GameSettings.difficulty}, Interval = {spawnInterval}");
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Debug.Log($"[SPAWN TRIGGER] Timer reached zero at {Time.time:F2}");
            SpawnEnemy();
            Debug.Log($"[Enemy Count]: {GameObject.FindObjectsOfType<Enemy>().Length}");
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        float xPos;
        do
        {
            xPos = Random.Range(-8f, 8f);
        }
        while (Mathf.Abs(xPos - GameManager.Instance.playerSpawnPoint.position.x) < 1.5f); // 플레이어 x 위치와 최소 거리 보장

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
