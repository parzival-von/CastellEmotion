using UnityEngine;

public class CamisaStartupLoader : MonoBehaviour
{
    public CamisaColorGlobalManager camisaManager;

    void Start()
    {
        int collaIndex = PlayerPrefs.GetInt("COLLA_SELECCIONADA", -1);

        Color[] colors = {
            new Color(1f, 0.72f, 0.02f),  // Bordegassos de Vilanova
            new Color(0.62f, 0f, 0.2f),   // Joves Xiquets de Valls
            new Color(0f, 0.43f, 0.26f)   // Castellers de Vilafranca
        };

        if (collaIndex >= 0 && collaIndex < colors.Length && camisaManager != null)
        {
            camisaManager.CanviarColorCamises(colors[collaIndex]);
        }
        else
        {
            Debug.LogWarning("⚠️ No s'ha trobat informació de la colla per aplicar el color.");
        }
    }
}
