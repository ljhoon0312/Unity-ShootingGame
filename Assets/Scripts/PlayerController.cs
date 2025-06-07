using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private PlayerAttack attack;

    void Start()
    {
        attack = GetComponent<PlayerAttack>();
        if (attack == null)
            Debug.LogError("PlayerAttack 컴포넌트를 찾을 수 없습니다.");
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            attack?.Fire();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            UseSpecialSkill();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, v, 0).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;

        // 화면 경계 제한
        ClampToScreen();
    }

    void ClampToScreen()
    {
        Vector3 pos = transform.position;
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float x = Mathf.Clamp(pos.x, min.x, max.x);
        float y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = new Vector3(x, y, pos.z);
    }

    void UseSpecialSkill()
    {
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(bullet);
        }
    }
}
