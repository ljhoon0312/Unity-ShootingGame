using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float bounceSpeed = 2f;         // �̵� �ӵ�
    public float minY = -0.3f;             // �Ʒ� �Ѱ� (��� ������)
    public float maxY = 0.3f;              // �� �Ѱ� (��� ������)

    private Vector3 startPos;
    private float direction = 1f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = transform.position.y + direction * bounceSpeed * Time.deltaTime;

        if (newY > startPos.y + maxY)
        {
            newY = startPos.y + maxY;
            direction = -1f; // �Ʒ��� ����
        }
        else if (newY < startPos.y + minY)
        {
            newY = startPos.y + minY;
            direction = 1f;  // ���� ����
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
