using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button startButton;

    private Image easyImage;
    private Image normalImage;
    private Image hardImage;

    private Color selectedColor = new Color(0.5f, 0.5f, 0.5f); // 어두운 색
    private Color defaultColor = Color.white;

    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        easyImage = easyButton.GetComponent<Image>();
        normalImage = normalButton.GetComponent<Image>();
        hardImage = hardButton.GetComponent<Image>();

        // 기본 선택
        SelectDifficulty("Normal");

        // 버튼에 리스너 연결
        easyButton.onClick.AddListener(() => SelectDifficulty("Easy"));
        normalButton.onClick.AddListener(() => SelectDifficulty("Normal"));
        hardButton.onClick.AddListener(() => SelectDifficulty("Hard"));
        startButton.onClick.AddListener(StartGame);
    }

    private void SelectDifficulty(string difficulty)
    {
        GameSettings.difficulty = difficulty;

        easyImage.color = (difficulty == "Easy") ? selectedColor : defaultColor;
        normalImage.color = (difficulty == "Normal") ? selectedColor : defaultColor;
        hardImage.color = (difficulty == "Hard") ? selectedColor : defaultColor;
    }

    private void StartGame()
    {
        Debug.Log($"게임 시작 - 선택된 난이도: {GameSettings.difficulty}");
        // Canvas 포함한 모든 UI 제거
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            Destroy(canvas);
        }

        GameObject uiManager = GameObject.Find("UIManager");
        if (uiManager != null)
        {
            Destroy(uiManager);
        }
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickButton()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
