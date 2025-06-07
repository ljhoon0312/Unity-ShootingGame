using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isInvincible = false;

    void Start()
    {
        StartInvincibility(); // 리스폰 시 무적 1초
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isInvincible) return;

        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject); // 플레이어 파괴

            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.OnPlayerDeath(); // GameManager에게 사망 알림
            }
        }
    }

    void StartInvincibility()
    {
        isInvincible = true;
        Invoke(nameof(EndInvincibility), 1.0f); // 1초 후 무적 해제
    }

    void EndInvincibility()
    {
        isInvincible = false;
    }
}
