using UnityEngine;
using System.Collections;

public class AmmoItemSpawner : MonoBehaviour
{
    public GameObject ammoItemPrefab;

    public float minX = -8f, maxX = 8f;
    public float minY = -4f, maxY = 4f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float wait = Random.Range(8f, 10f);
            yield return new WaitForSeconds(wait);

            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            Instantiate(ammoItemPrefab, spawnPos, Quaternion.identity);
        }
    }
}
