using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject youWonUI;

    private void Start()
    {
        gameOverUI.SetActive(false);
        youWonUI.SetActive(false);

    }

    public void TriggerGameOver()
    {

        gameOverUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void TriggerYouWon()
    {

        youWonUI.SetActive(true);


        Time.timeScale = 0f;
    }
}
