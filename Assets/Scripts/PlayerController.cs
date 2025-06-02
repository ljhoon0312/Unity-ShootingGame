using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerNumber { Player1, Player2 }
    public PlayerNumber playerNumber = PlayerNumber.Player1;

    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool canUseUltimate = false;  // 필살기 사용 가능 여부

    void HandleMovement()
    {
        float h = 0;
        float v = 0;

        if (playerNumber == PlayerNumber.Player1)
        {
            h = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
            v = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        }
        else if (playerNumber == PlayerNumber.Player2)
        {
            h = Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
            v = Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0;
        }

        Vector3 moveDir = new Vector3(h, v, 0).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void HandleInput()
    {
        if (playerNumber == PlayerNumber.Player1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();

            if (Input.GetKeyDown(KeyCode.X) && canUseUltimate)
            {
                UseUltimate();
                canUseUltimate = false; // 한 번 사용 후 비활성화
            }
        }
        else if (playerNumber == PlayerNumber.Player2)
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
                Shoot();

            if (Input.GetKeyDown(KeyCode.RightShift) && canUseUltimate)
            {
                UseUltimate();
                canUseUltimate = false;
            }
        }
    }

    public void EnableUltimate()
    {
        canUseUltimate = true;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    void UseUltimate()
    {
        Debug.Log($"{playerNumber} used ULTIMATE!");
        // 여기서 적 탄환 전부 제거하는 로직 넣으면 됨
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInput();
    }
}
