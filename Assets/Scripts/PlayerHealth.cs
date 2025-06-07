using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isInvincible = false;

    void Start()
    {
        StartInvincibility(); // ������ �� ���� 1��
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isInvincible) return;

        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject); // �÷��̾� �ı�

            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.OnPlayerDeath(); // GameManager���� ��� �˸�
            }
        }
    }

    void StartInvincibility()
    {
        isInvincible = true;
        Invoke(nameof(EndInvincibility), 1.0f); // 1�� �� ���� ����
    }

    void EndInvincibility()
    {
        isInvincible = false;
    }
}
