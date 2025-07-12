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
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("♻️ GameManager duplicat destruït a escena: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("✅ GameManager persistent inicialitzat");
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Nou estat del joc: " + newState.ToString());

        // Aquí pots afegir accions específiques per cada estat
        switch (newState)
        {
            case GameState.MainMenu:
                // Accions específiques per MainMenu
                break;
            case GameState.Profile:
                // Carregar dades de perfil
                break;
            case GameState.ExploringLimits:
                // Preparar simulacions
                break;
            case GameState.Training:
                // Preparar escena d'entrenament
                break;
            case GameState.Paused:
                // Pausar simulació
                break;
        }
    }
}
