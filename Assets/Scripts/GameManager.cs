using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameTime = 120f;
    public TextMesh timerText;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    public TextMesh lifeText;

    private int playerLife = 3;
    private bool bossSpawned = false;

    void Start()
    {
        SpawnPlayer();
        UpdateLifeUI();
    }

    void Update()
    {
        gameTime -= Time.deltaTime;
        if (gameTime < 0f) gameTime = 0f;

        timerText.text = "Time: " + Mathf.CeilToInt(gameTime) + "s";

        if (!bossSpawned && gameTime <= 60f)
        {
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            bossSpawned = true;
        }
    }

    public void OnPlayerDeath()
    {
        playerLife--;
        UpdateLifeUI();

        if (playerLife > 0)
        {
            Invoke(nameof(SpawnPlayer), 1.0f); // 1초 후 리스폰
        }
        else
        {
            // 게임 오버 처리
            Debug.Log("Game Over!");
        }
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    void UpdateLifeUI()
    {
        lifeText.text = "Life: " + playerLife;
    }
}
