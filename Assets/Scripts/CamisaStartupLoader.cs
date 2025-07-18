using UnityEngine;

public class CamisaStartupLoader : MonoBehaviour
{
    public CamisaColorGlobalManager camisaManager;

    void Start()
    {
        int collaIndex = PlayerPrefs.GetInt("COLLA_SELECCIONADA", -1);

        // Colores por colla (orden según index)
        Color[] colors = {
            new Color(1f, 0.72f, 0.02f),    // 0 - Bordegassos de Vilanova (groc mostassa)
            new Color(0.62f, 0f, 0.2f),     // 1 - Joves Xiquets de Valls (grana)
            new Color(0f, 0.43f, 0.26f),    // 2 - Castellers de Vilafranca (verd)
            new Color(0.14f, 0.25f, 0.78f), // 3 - Ganàpies de la UAB (blau osteosa)
            new Color(0.18f, 0.36f, 0.25f)  // 4 - Arreplegats de la Zona Universitària (verd quiròfan)
        };

        if (collaIndex >= 0 && collaIndex < colors.Length && camisaManager != null)
        {
            camisaManager.CanviarColorCamises(colors[collaIndex]);
            Debug.Log("✔️ Color de la colla aplicat correctament.");
        }
        else
        {
            Debug.LogWarning("⚠️ No s'ha trobat informació de la colla per aplicar el color.");
        }
    }
}
