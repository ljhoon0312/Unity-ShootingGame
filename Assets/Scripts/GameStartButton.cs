using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public void OnClickStart()
    {
        Debug.Log("선택된 난이도: " + GameSettings.difficulty);
        SceneManager.LoadScene("GameScene");
    }
}
