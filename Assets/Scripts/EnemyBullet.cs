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
            // �÷��̾� �ǰ� �� �ı�
            Destroy(other.gameObject); // �÷��̾�
            Destroy(gameObject);       // �Ѿ�
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // ȭ�� �� ������ ����
    }
}
