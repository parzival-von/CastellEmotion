using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class BalanceBoardOccupancyDetector : MonoBehaviour
{
    [Header("Parámetros de detección")]
    [Tooltip("Peso mínimo en kg para considerar que alguien está subido")]
    public float thresholdKg = 5f;

    [Tooltip("Número de frames consecutivos por encima/por debajo del umbral para confirmar el cambio de estado")]
    public int requiredFrames = 5;

    [Header("Acciones al bajar de la taula")]
    [SerializeField] private Transform xrRig;
    [SerializeField] private Transform posicioInicial;
    [SerializeField] private CanvasGroup pantallaNegra;
    [SerializeField] private AudioSource soCaiguda;

    private bool _isOccupied = false;
    private int _framesAbove = 0;
    private int _framesBelow = 0;

    void Update()
    {
        if (Joystick.all.Count == 0) return;
        var stick = Joystick.all[0];

        // Lectura de los 4 sensores (normalizados 0…1)
        float z = stick.TryGetChildControl<AxisControl>("z")?.ReadValue() ?? 0f;
        float rx = stick.TryGetChildControl<AxisControl>("rx")?.ReadValue() ?? 0f;
        float ry = stick.TryGetChildControl<AxisControl>("ry")?.ReadValue() ?? 0f;
        float rz = stick.TryGetChildControl<AxisControl>("rz")?.ReadValue() ?? 0f;

        // Convierte normalizado -> kg (ajusta maxKg a tu calibración)
        const float maxKg = 40f;
        float kgTL = Mathf.Clamp01(z) * maxKg;
        float kgTR = Mathf.Clamp01(rx) * maxKg;
        float kgBL = Mathf.Clamp01(ry) * maxKg;
        float kgBR = Mathf.Clamp01(rz) * maxKg;
        float totalKg = kgTL + kgTR + kgBL + kgBR;

        // Detección con histéresis (para evitar flicker)
        if (totalKg >= thresholdKg)
        {
            _framesAbove++;
            _framesBelow = 0;
            if (!_isOccupied && _framesAbove >= requiredFrames)
            {
                _isOccupied = true;
                OnBoard();
            }
        }
        else
        {
            _framesBelow++;
            _framesAbove = 0;
            if (_isOccupied && _framesBelow >= requiredFrames)
            {
                _isOccupied = false;
                OffBoard();
            }
        }
    }

    private void OnBoard()
    {
        Debug.Log($"▶️ Usuari ha pujat (totalKg = {_getTotalKg():F1} kg)");
        // Pots afegir accions de benvinguda aquí
    }

    private void OffBoard()
    {
        Debug.Log($"⏸️ Usuari ha baixat (totalKg = {_getTotalKg():F1} kg)");

        if (soCaiguda != null) soCaiguda.Play();

        if (pantallaNegra != null)
            StartCoroutine(FadeInNegre());

        if (xrRig != null && posicioInicial != null)
            StartCoroutine(RecolocarXR());
    }

    private float _getTotalKg()
    {
        var stick = Joystick.all[0];
        float z = stick.TryGetChildControl<AxisControl>("z")?.ReadValue() ?? 0f;
        float rx = stick.TryGetChildControl<AxisControl>("rx")?.ReadValue() ?? 0f;
        float ry = stick.TryGetChildControl<AxisControl>("ry")?.ReadValue() ?? 0f;
        float rz = stick.TryGetChildControl<AxisControl>("rz")?.ReadValue() ?? 0f;
        const float maxKg = 40f;
        return Mathf.Clamp01(z) * maxKg
             + Mathf.Clamp01(rx) * maxKg
             + Mathf.Clamp01(ry) * maxKg
             + Mathf.Clamp01(rz) * maxKg;
    }

    private IEnumerator FadeInNegre()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            pantallaNegra.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
    }

    private IEnumerator RecolocarXR()
    {
        yield return new WaitForSeconds(1.5f); // espera abans de reposar
        xrRig.position = posicioInicial.position;
        xrRig.rotation = posicioInicial.rotation;

        // Fade out
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            pantallaNegra.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
    }
}
