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

        // ��� ���� ���� + ���̱� ����
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        // ���� ����� ���� Instantiate �� �÷��̾�� ���� ������Ʈ
        if (explosionSoundPrefab != null)
        {
            GameObject sfx = Instantiate(explosionSoundPrefab, transform.position, Quaternion.identity);
            DontDestroyOnLoad(sfx); // Ȥ�� Destroy�� ���� ����
            Destroy(sfx, 2f); // 2�� �ڿ� �ڵ� ����
        }

        if (GameManager.Instance.playerLife == 0)
        {
            GameManager.Instance.GameOver("You Died!");
            Destroy(gameObject); // ������ ���� ���ŵ�
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
