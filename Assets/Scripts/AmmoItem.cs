using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    public AudioClip pickupSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            PlayerAttack attack = other.GetComponent<PlayerAttack>();
            if (attack != null)
            {
                attack.IncreaseBulletCount();
            }
            Destroy(gameObject);
        }
    }
}
