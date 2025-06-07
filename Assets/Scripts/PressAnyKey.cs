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
        // �ƹ� Ű�� ������ ���� ������
        if (!started && Input.anyKeyDown)
        {
            started = true;
            StartCoroutine(LoadNextSceneDelayed());
        }
    }

    System.Collections.IEnumerator LoadNextSceneDelayed()
    {
        yield return null; // �� ������ ��ٸ� (Awake ����� �ð� Ȯ��)
        SceneManager.LoadScene("ModeSelectScene");
    }
}
