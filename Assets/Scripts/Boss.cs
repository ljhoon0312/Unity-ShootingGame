using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 500;
    private int currentHealth;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 7f;
    public float fireRate = 1.0f;
    public int bulletCount = 27;
    public float spreadAngle = 180f;

    public float moveSpeed = 2f;
    public float minY = 2.5f;

    private bool isPhase2 = false;
    private bool isPhase3 = false;

    public AudioClip explosionSFX;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        InvokeRepeating(nameof(FireSpread), 1f, fireRate);
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        // �Ʒ��� �������� ����
        pos.y -= moveSpeed * Time.deltaTime;

        if (pos.y <= minY)
        {
            pos.y = minY;
            moveSpeed = 0; // ����
        }

        transform.position = pos;

        // ü�¿� ���� ������ ��ȭ
        if (!isPhase2 && currentHealth <= maxHealth * 0.66f)
        {
            isPhase2 = true;
            bulletCount += 6;
            fireRate = 0.7f;
            CancelInvoke();
            InvokeRepeating(nameof(FireSpread), 0.5f, fireRate);
        }
        else if (!isPhase3 && currentHealth <= maxHealth * 0.33f)
        {
            isPhase3 = true;
            bulletCount += 6;
            bulletSpeed += 2f;
            fireRate = 0.5f;
            CancelInvoke();
            InvokeRepeating(nameof(FireSpread), 0.3f, fireRate);
        }
    }

    void FireSpread()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = -spreadAngle / 2f + (spreadAngle / (bulletCount - 1)) * i;
            Vector3 dir = Quaternion.Euler(0f, 0f, angle) * Vector3.down;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
            bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, 0);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = dir.normalized * bulletSpeed;
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log("Boss HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            if (explosionSFX != null)
                AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
            Debug.Log("Boss Defeated");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage();
            }
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
