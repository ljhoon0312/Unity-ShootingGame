using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack attack = other.GetComponent<PlayerAttack>();
            if (attack != null)
            {
                attack.IncreaseBulletCount();
            }
            Destroy(gameObject);
        }
    }
}
