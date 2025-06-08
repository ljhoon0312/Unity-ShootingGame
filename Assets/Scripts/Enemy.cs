using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionSoundPrefab;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float speed = 2f;

    void Start()
    {
        InvokeRepeating("Fire", 1f, 2f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -15f)
        {
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().isBossBullet = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("[Enemy] 충돌 감지됨: " + other.name + ", 태그: " + other.tag);

        // 플레이어 총알에 맞은 경우
        if (other.CompareTag("PlayerBullet"))
        {
            if (explosionSoundPrefab != null)
            {
                Debug.Log("Enemy 총알 맞고 소리 재생됨 by " + gameObject.name);
                GameObject sfx = Instantiate(explosionSoundPrefab, transform.position, Quaternion.identity);
                Destroy(sfx, 2f);
            }

            Destroy(other.gameObject);  // 총알 제거
            Destroy(gameObject);        // 적 제거
            GameManager.Instance.AddScore(30);  // 스코어 30점 추가
        }

        // 플레이어 본체랑 충돌했을 때 (기존 코드 유지)
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null && !ph.IsInvincible())
            {
                ph.TakeDamage(); // 여기서 알아서 파괴 + 리스폰 처리

                if (explosionSoundPrefab != null)
                {
                    GameObject sfx = Instantiate(explosionSoundPrefab, transform.position, Quaternion.identity);
                    Destroy(sfx, 2f);
                }

                Destroy(gameObject); // 적은 자기 자신만 죽음
            }
        }
    }

        void OnDestroy()
    {
        Debug.Log($"[DESTROY] Enemy destroyed at {Time.time:F2}");
    }
}
