using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float bounceSpeed = 2f;         // 이동 속도
    public float minY = -0.3f;             // 아래 한계 (상대 오프셋)
    public float maxY = 0.3f;              // 위 한계 (상대 오프셋)

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
            direction = -1f; // 아래로 반전
        }
        else if (newY < startPos.y + minY)
        {
            newY = startPos.y + minY;
            direction = 1f;  // 위로 반전
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
