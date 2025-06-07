using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    private bool started = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 아무 키나 누르면 다음 씬으로
        if (!started && Input.anyKeyDown)
        {
            started = true;
            StartCoroutine(LoadNextSceneDelayed());
        }
    }

    System.Collections.IEnumerator LoadNextSceneDelayed()
    {
        yield return null; // 한 프레임 기다림 (Awake 실행될 시간 확보)
        SceneManager.LoadScene("ModeSelectScene");
    }
}
