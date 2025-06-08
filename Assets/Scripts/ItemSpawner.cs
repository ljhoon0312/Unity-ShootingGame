using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject lifeItemPrefab;

    public float minSpawnTime = 10f;
    public float maxSpawnTime = 20f;

    public float minX = -8f;
    public float maxX = 8f;
    public float minY = 0f;
    public float maxY = 4f;

    void Start()
    {
        StartCoroutine(SpawnItemRoutine());
    }

    System.Collections.IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPos = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                0f
            );

            Instantiate(lifeItemPrefab, spawnPos, Quaternion.identity);
        }
    }
}
