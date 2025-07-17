using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        MainMenu,
        Profile,
        ExploringLimits,
        Training,
        Paused
    }

    public GameState CurrentState { get; private set; } = GameState.MainMenu;

    private void Awake()
    {
        // Cada escena tendrá su propio GameManager independiente
        Instance = this;
        Debug.Log("🧩 GameManager de escena inicialitzat: " + gameObject.scene.name);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Nou estat del joc: " + newState.ToString());

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Profile:
                break;
            case GameState.ExploringLimits:
                break;
            case GameState.Training:
                break;
            case GameState.Paused:
                break;
        }
    }
}
