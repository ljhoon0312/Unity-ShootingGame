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
        // 아래로 이동
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // 공격 타이밍
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireInterval;
        }

        // 화면 아래로 사라지면 제거
        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Vector3 spawnPos = firePoint.position;
            spawnPos.z = 0f; // 반드시 Z = 0
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어와 충돌 시 둘 다 파괴
            Destroy(other.gameObject); // 플레이어
            Destroy(gameObject);       // 적
            // TODO: GameManager에서 게임오버 호출하도록 연동 가능
        }
    }
}
