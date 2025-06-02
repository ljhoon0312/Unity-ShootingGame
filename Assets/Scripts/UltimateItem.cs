using UnityEngine;

public class UltimateItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.EnableUltimate();
            Destroy(gameObject); // 아이템 제거
        }
    }
}
