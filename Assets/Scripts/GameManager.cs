using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("게임 시간")]
    public float gameTime = 120f;
    public TMP_Text timerText;

    [Header("플레이어")]
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    public int playerLife = 3;
    public TMP_Text lifeText;

    [Header("보스")]
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public TMP_Text bossWarningText;
    private bool bossSpawned = false;
    private bool bossWarningShown = false;

    [Header("게임 상태")]
    private bool isGameOver = false;

    [Header("게임 브금")]
    public AudioClip gameSceneBGM;
    public AudioClip bossBGM;
    private bool bossMusicPlayed = false;

    [Header("게임 오버 UI")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    [Header("게임 스코어")]
    public TMP_Text scoreText;
    public int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        gameTime = 90f;  // 여기 반드시 90초로 시작
        BGMManager.instance?.PlayBGM(gameSceneBGM);
        Time.timeScale = 1f;
        SpawnPlayer();
        UpdateLifeUI();
        UpdateScoreUI();
        gameOverPanel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        bossWarningText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (isGameOver) return;

        gameTime -= Time.deltaTime;
        gameTime = Mathf.Max(0f, gameTime); // 음수 방지
        timerText.text = "Time: " + Mathf.CeilToInt(gameTime);

        if (!bossMusicPlayed && gameTime <= 0f)
        {
            BGMManager.instance?.PlayBGM(bossBGM);
            bossMusicPlayed = true;
        }

        if (!bossWarningShown && gameTime <= 60f)
        {
            bossWarningShown = true;
            StartCoroutine(ShowBossWarning());
        }

        if (gameTime <= 0f)
        {
            GameOver("Time's up!");
        }
    }

    public IEnumerator RespawnPlayerCoroutine(float delay, GameObject oldPlayer)
    {
        oldPlayer.SetActive(false); // 즉시 화면에서 사라짐

        yield return new WaitForSeconds(delay);

        SpawnPlayer();
        Destroy(oldPlayer); // 프레임 기다리지 않고 바로 삭제됨
    }

    public void OnPlayerDeath()
    {
        playerLife--;
        UpdateLifeUI();
        UpdateScoreUI();

        if (playerLife <= 0)
        {
            GameOver("You Died!");
        }
        else
        {
            Invoke(nameof(SpawnPlayer), 1.0f);
        }
    }

    public void SpawnPlayer()
    {
        Vector3 pos = playerSpawnPoint.position;
        pos.z = 0f;
        GameObject newPlayer = Instantiate(playerPrefab, pos, Quaternion.identity);

        // 리스폰 후 무적 적용
        PlayerHealth ph = newPlayer.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.SetInvincible(true);
            StartCoroutine(DisableInvincibilityAfterSeconds(ph, 3f)); // 3초 무적
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        if (score < 0) score = 0;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLifeUI()
    {
        lifeText.text = "Life : " + playerLife;
    }

    public void GameOver(string message)
    {
        isGameOver = true;
        Time.timeScale = 0f; // 게임 정지
        gameOverText.text = message;
        gameOverText.gameObject.SetActive(true);
        gameOverPanel.SetActive(true);
    }

    IEnumerator ShowBossWarning()
    {
        bossWarningText.text = "BOSS INCOMING";
        bossWarningText.gameObject.SetActive(true);

        float duration = 2f;
        float time = 0f;

        while (time < duration)
        {
            float scale = 1f + Mathf.PingPong(time * 3f, 0.5f);
            bossWarningText.rectTransform.localScale = Vector3.one * scale;
            time += Time.deltaTime;
            yield return null;
        }

        bossWarningText.gameObject.SetActive(false);

        if (!bossSpawned)
        {
            Vector3 spawnPos = bossSpawnPoint.position;
            spawnPos.z = 0f;
            Instantiate(bossPrefab, spawnPos, Quaternion.identity);
            bossSpawned = true;
        }
    }

    IEnumerator DisableInvincibilityAfterSeconds(PlayerHealth ph, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ph.SetInvincible(false);
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroScene"); // 메인 씬 이동
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 테스트용 종료
#endif
    }
}
