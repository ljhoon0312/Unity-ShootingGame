using UnityEngine;

public class LifeItem : MonoBehaviour
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
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.AddLife();  // PlayerHealth.cs�� AddLife() ������ �־�� ��
            }
            Destroy(gameObject);  // ������ �����
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
