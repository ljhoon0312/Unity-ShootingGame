using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    private bool isInvincible = false;
    private Vector3 spawnPosition = new Vector3(0, -3, 0);
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    public AudioClip explosionSFX;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage()
    {
        if (isInvincible || lives <= 0) return;

        lives--;

        if (explosionSFX != null)
            AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

        GameManager.Instance.OnPlayerDeath();

        if (lives > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    IEnumerator Respawn()
    {
        isInvincible = true;

        // 숨김 처리
        spriteRenderer.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(3f);

        Destroy(gameObject); // 먼저 자신을 제거

        yield return null;   // 프레임 하나 쉬고 나서

        GameManager.Instance.SpawnPlayer(); // 그 후 새 전투기 생성
    }

    public void AddLife()
    {
        GameManager.Instance.playerLife++;
        GameManager.Instance.UpdateLifeUI();
    }
}
