using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Oyunu durdur
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyunu yeniden baþlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Geçerli sahneyi yeniden yükle
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Oyunu yeniden baþlat
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesini yükle
    }
}
