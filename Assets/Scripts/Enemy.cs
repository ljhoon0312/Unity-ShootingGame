using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip explosionSFX;
    private AudioSource audioSource;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float speed = 2f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null && !ph.IsInvincible()) // ＄ 公利老 锭绰 公矫
            {
                if (explosionSFX != null)
                    AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

                ph.TakeDamage();
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        Debug.Log($"[DESTROY] Enemy destroyed at {Time.time:F2}");
    }
}
