using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
            Destroy(gameObject, audio.clip.length + 0.1f); // 소리 끝나면 삭제
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
