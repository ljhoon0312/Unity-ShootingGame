using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;
    public bool isBossBullet = false;

    void Update()
    {
        Vector3 direction = isBossBullet ? transform.up : Vector3.down;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Z ��ġ ���� ����
        Vector3 pos = transform.position;
        pos.z = 0f;
        transform.position = pos;

        // ȭ�� ������ ������ ����
        if (pos.y < -6f || pos.y > 6f || pos.x < -10f || pos.x > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage();
            Destroy(gameObject);
        }
    }
}
