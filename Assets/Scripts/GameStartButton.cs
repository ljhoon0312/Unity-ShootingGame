using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public void OnClickStart()
    {
        Debug.Log("���õ� ���̵�: " + GameSettings.difficulty);
        SceneManager.LoadScene("GameScene");
    }
}
