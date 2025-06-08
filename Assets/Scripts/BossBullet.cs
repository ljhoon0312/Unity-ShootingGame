using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject explosionSoundPrefab;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null && !player.IsInvincible())
            {
                player.TakeDamage();
            }

            if (explosionSoundPrefab != null)
            {
                GameObject sfx = Instantiate(explosionSoundPrefab, transform.position, Quaternion.identity);
                Destroy(sfx, 2f);
            }

            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        if (transform.position.y < -10f || transform.position.y > 15f || Mathf.Abs(transform.position.x) > 15f)
        {
            Destroy(gameObject);
        }
    }
}
