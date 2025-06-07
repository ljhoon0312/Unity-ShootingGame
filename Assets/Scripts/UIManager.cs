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

    private Color selectedColor = new Color(0.5f, 0.5f, 0.5f); // ��ο� ��
    private Color defaultColor = Color.white;

    private void Start()
    {
        easyImage = easyButton.GetComponent<Image>();
        normalImage = normalButton.GetComponent<Image>();
        hardImage = hardButton.GetComponent<Image>();

        // �⺻ ����
        SelectDifficulty("Normal");

        // ��ư�� ������ ����
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
        Debug.Log($"���� ���� - ���õ� ���̵�: {GameSettings.difficulty}");
        SceneManager.LoadScene("GameScene");
    }
}
