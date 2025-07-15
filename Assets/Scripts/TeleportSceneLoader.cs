using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportSceneLoader : MonoBehaviour
{
    [Tooltip("Nombre de la escena que se cargará al teletransportarse aquí.")]
    public string nombreEscena = "MainMenu";

    private void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que el objeto que entra sea el jugador
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
