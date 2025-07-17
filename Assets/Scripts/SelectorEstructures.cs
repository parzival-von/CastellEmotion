using UnityEngine;

public class SelectorEstructures : MonoBehaviour
{
    [Header("Estructures")]
    public GameObject pilar;
    public GameObject torreDos;
    public GameObject tres;

    [Header("Color camises")]
    public CamisaColorGlobalManager camisaManager;

    private void Start()
    {
        DesactivaTotes();
    }

    void DesactivaTotes()
    {
        if (pilar) pilar.SetActive(false);
        if (torreDos) torreDos.SetActive(false);
        if (tres) tres.SetActive(false);
    }

    public void ActivaPilar()
    {
        DesactivaTotes();
        if (pilar) pilar.SetActive(true);
        AplicaColor();
    }

    public void ActivaTorreDos()
    {
        DesactivaTotes();
        if (torreDos) torreDos.SetActive(true);
        AplicaColor();
    }

    public void ActivaTres()
    {
        DesactivaTotes();
        if (tres) tres.SetActive(true);
        AplicaColor();
    }

    void AplicaColor()
    {
        if (camisaManager == null) return;

        int collaIndex = PlayerPrefs.GetInt("COLLA_SELECCIONADA", -1);

        Color[] colors = {
            new Color(1f, 0.72f, 0.02f),  // Bordegassos
            new Color(0.62f, 0f, 0.2f),   // Joves Valls
            new Color(0f, 0.43f, 0.26f)   // Vilafranca
        };

        if (collaIndex >= 0 && collaIndex < colors.Length)
        {
            camisaManager.CanviarColorCamises(colors[collaIndex]);
        }
    }
}
