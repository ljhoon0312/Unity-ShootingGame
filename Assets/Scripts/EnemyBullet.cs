using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 피격 시 파괴
            Destroy(other.gameObject); // 플레이어
            Destroy(gameObject);       // 총알
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // 화면 밖 나가면 제거
    }
}
