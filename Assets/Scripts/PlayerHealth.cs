using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    private bool isInvincible = false;
    private Vector3 spawnPosition = new Vector3(0, -3, 0);
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    public GameObject explosionSoundPrefab;
    private bool isDead = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage()
    {
        if (isDead || GameManager.Instance.playerLife <= 0) return;
        isDead = true;
        GameManager.Instance.playerLife--;
        GameManager.Instance.AddScore(-20);
        GameManager.Instance.UpdateLifeUI();
        GameManager.Instance.UpdateScoreUI();

        // 즉시 조작 차단 + 보이기 제거
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        // 폭발 사운드는 따로 Instantiate → 플레이어랑 별도 오브젝트
        if (explosionSoundPrefab != null)
        {
            GameObject sfx = Instantiate(explosionSoundPrefab, transform.position, Quaternion.identity);
            DontDestroyOnLoad(sfx); // 혹시 Destroy될 위험 방지
            Destroy(sfx, 2f); // 2초 뒤에 자동 제거
        }

        if (GameManager.Instance.playerLife == 0)
        {
            GameManager.Instance.GameOver("You Died!");
            Destroy(gameObject); // 프레임 끝에 제거됨
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.SpawnPlayer();
        Destroy(gameObject);
    }

    public void AddLife()
    {
        GameManager.Instance.playerLife++;
        GameManager.Instance.UpdateLifeUI();
    }
    public bool IsInvincible()
    {
        return isInvincible;
    }
    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }
}
