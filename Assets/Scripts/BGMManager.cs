using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource bgmSource;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (bgmSource != null && !bgmSource.isPlaying)
            {
                bgmSource.volume = 0.1f; // 볼륨 조절
                bgmSource.loop = true;
                bgmSource.Play();
                Debug.Log("BGM 시작: " + bgmSource.clip.name);
            }
        }
        else
        {
            Destroy(gameObject);
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
