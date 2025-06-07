using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;

    private float fireTimer;

    void Update()
    {
        // �Ʒ��� �̵�
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // ���� Ÿ�̹�
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireInterval;
        }

        // ȭ�� �Ʒ��� ������� ����
        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Vector3 spawnPos = firePoint.position;
            spawnPos.z = 0f; // �ݵ�� Z = 0
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾�� �浹 �� �� �� �ı�
            Destroy(other.gameObject); // �÷��̾�
            Destroy(gameObject);       // ��
            // TODO: GameManager���� ���ӿ��� ȣ���ϵ��� ���� ����
        }
    }
}
