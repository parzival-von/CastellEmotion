using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadProfile()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Profile);
        SceneManager.LoadScene("Profile");
    }

    public void LoadExplore()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.ExploringLimits);
        SceneManager.LoadScene("Explore");
    }

    public void LoadTrain()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Training);
        SceneManager.LoadScene("Train");
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Sortint del joc...");
        Application.Quit();
    }
}
