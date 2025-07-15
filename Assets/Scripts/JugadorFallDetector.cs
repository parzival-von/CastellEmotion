using UnityEngine;

public class JugadorFallDetector : MonoBehaviour
{
    public float alturaMinima = -1.5f;
    public CastellerManager castellerManager; // referència al gestor de castellers

    private bool haCaigut = false;

    void Update()
    {
        if (!haCaigut && transform.position.y < alturaMinima)
        {
            haCaigut = true;
            Debug.Log("❌ Jugador ha caigut! Caiguda general.");
            castellerManager.FerCaureTots();
        }
    }
}
