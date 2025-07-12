using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CoPRecoveryVisualizer : MonoBehaviour
{
    [Header("Referències UI")]
    public RectTransform targetZone;  // cercle de referència
    public RectTransform copDot;      // punt mòbil

    [Header("Paràmetres")]
    [Tooltip("Quina fracció del radi és la zona vàlida (0.1–1)")]
    [Range(0.1f, 1f)]
    public float validRadius = 0.5f;
    [Tooltip("Segons que has de mantenir-te dins per considerar-ho correcte")]
    public float requiredTime = 2f;
    [Tooltip("Tecla per calibrar la postura d'inici (referència)")]
    public Key calibrationKey = Key.R;  // <-- Cambiat a R!

    private Vector2 initialCenter;
    private bool isCalibrated = false;
    private float insideTimer = 0f;

    void Update()
    {
        if (Joystick.all.Count == 0) return;
        var stick = Joystick.all[0];

        // Llegim el CoP normalitzat [-1,1]
        Vector2 cop = new Vector2(
            stick.stick.x.ReadValue(),
            stick.stick.y.ReadValue()
        );

        // Fase de calibració de referència
        if (!isCalibrated)
        {
            if (Keyboard.current[calibrationKey].wasPressedThisFrame)
            {
                initialCenter = cop;
                isCalibrated = true;
                Debug.Log($"🟢 Postura inicial registrada: {initialCenter}");
            }
            return;
        }

        // CoP relatiu al centre referència
        Vector2 rel = cop - initialCenter;

        // Movem el punt dins del cercle
        float radius = targetZone.rect.width * 0.5f;
        Vector2 dotPos = rel * (radius - copDot.rect.width * 0.5f);
        copDot.anchoredPosition = dotPos;

        // Comprovem si dins la zona vàlida
        if (rel.magnitude <= validRadius)
        {
            insideTimer += Time.deltaTime;
            copDot.GetComponent<Image>().color = Color.green;
        }
        else
        {
            insideTimer = 0f;
            copDot.GetComponent<Image>().color = Color.red;
        }

        // Si t'hi has mantingut prou temps
        if (insideTimer >= requiredTime)
            OnRecovered();
    }

    private void OnRecovered()
    {
        Debug.Log("✅ Has recuperat la postura correcta!");
        enabled = false;
    }
}
