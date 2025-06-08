using UnityEngine;

public class BossBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);

            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
                gm.OnPlayerDeath();

            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
