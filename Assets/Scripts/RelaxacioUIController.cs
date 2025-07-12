using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelaxacioUIController : MonoBehaviour
{
    [Header("Referències UI")]
    public CanvasGroup sorollUI;
    public Image sorollIndicador;
    public CanvasGroup relaxacioUI;
    public TextMeshProUGUI textRelaxacio;
    public Transform cameraRig;
    public float distanciaUI = 1.5f;

    [Header("Paràmetres")]
    public float sorollActual = 0f;
    public float llindarMostraUI = 0.6f;
    public string[] frasesRelaxacio;
    private int fraseIndex = 0;

    private bool modeRelax = false;

    void Start()
    {
        HideCanvas(sorollUI);
        HideCanvas(relaxacioUI);
    }

    void Update()
    {
        ActualitzaPosicioUI();

        // Mostra UI de soroll si supera llindar
        if (sorollActual > llindarMostraUI)
        {
            ShowCanvas(sorollUI);
            ActualitzaColorSoroll(sorollActual);
        }
        else
        {
            HideCanvas(sorollUI);
        }

        // Entrada manual al mode relaxació (ex: tecla R o gest futur)
        if (Input.GetKeyDown(KeyCode.R))
        {
            ActivaRelaxacio();
        }
    }

    void ActualitzaColorSoroll(float valor)
    {
        if (valor < 0.5f)
            sorollIndicador.color = Color.green;
        else if (valor < 0.8f)
            sorollIndicador.color = Color.yellow;
        else
            sorollIndicador.color = Color.red;
    }

    void ActivaRelaxacio()
    {
        modeRelax = true;
        ShowCanvas(relaxacioUI);
        fraseIndex = (fraseIndex + 1) % frasesRelaxacio.Length;
        textRelaxacio.text = frasesRelaxacio[fraseIndex];
        Invoke(nameof(DesactivaRelaxacio), 6f); // Dura 6 segons
    }

    void DesactivaRelaxacio()
    {
        modeRelax = false;
        HideCanvas(relaxacioUI);
    }

    void ShowCanvas(CanvasGroup cg)
    {
        cg.alpha = Mathf.Lerp(cg.alpha, 1f, Time.deltaTime * 10);
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    void HideCanvas(CanvasGroup cg)
    {
        cg.alpha = Mathf.Lerp(cg.alpha, 0f, Time.deltaTime * 10);
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    void ActualitzaPosicioUI()
    {
        if (cameraRig != null)
        {
            Vector3 offset = cameraRig.forward * distanciaUI + cameraRig.up * -0.2f;
            transform.position = cameraRig.position + offset;
            transform.LookAt(cameraRig.position);
            transform.rotation = Quaternion.LookRotation(transform.position - cameraRig.position);
        }
    }
}

