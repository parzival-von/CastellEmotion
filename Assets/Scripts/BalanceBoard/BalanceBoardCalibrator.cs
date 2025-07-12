using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BalanceBoardTwoPointCalibrator : MonoBehaviour
{
    [Header("Masa conocida para calibración baja (kg)")]
    public float massLow = 20f;
    [Header("Masa aproximada para calibración alta (kg)")]
    public float massHigh = 57f;

    private float tareNorm = 0f;
    private float normLow = 0f, normHigh = 0f;
    private bool hasTare = false, hasLow = false, hasHigh = false;
    private float a = 1f, b = 0f;

    void Update()
    {
        if (Joystick.all.Count == 0) return;
        var stick = Joystick.all[0];

        // Suma normalizada de los 4 sensores (0…1)
        float sumNorm = ReadAxis(stick, "z")
                      + ReadAxis(stick, "rx")
                      + ReadAxis(stick, "ry")
                      + ReadAxis(stick, "rz");

        // 1) TARE (peso=0)
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            tareNorm = sumNorm;
            hasTare = true;
            Debug.Log($"🟢 Tare hecho (norm = {tareNorm:F3})");
        }

        // 2) CALIB baja (massLow)
        if (Keyboard.current.cKey.wasPressedThisFrame && hasTare)
        {
            normLow = sumNorm - tareNorm;
            hasLow = true;
            Debug.Log($"🟡 Calib baja (normLow = {normLow:F3} → {massLow} kg)");
        }

        // 3) CALIB alta (massHigh)
        if (Keyboard.current.vKey.wasPressedThisFrame && hasTare)
        {
            normHigh = sumNorm - tareNorm;
            hasHigh = true;
            Debug.Log($"🟠 Calib alta (normHigh = {normHigh:F3} → {massHigh} kg)");
        }

        // 4) Calcula pendientes cuando tengamos ambos puntos
        if (hasLow && hasHigh)
        {
            // a = (y2 - y1) / (x2 - x1)
            a = (massHigh - massLow) / (normHigh - normLow);
            // b = y1 - a * x1
            b = massLow - a * normLow;
        }

        // 5) Mostrar peso en tiempo real si calibrado
        if (hasTare && hasLow && hasHigh)
        {
            float realNorm = sumNorm - tareNorm;
            float totalKg = Mathf.Max(0f, a * realNorm + b);
            Debug.Log($"CoP norm = {realNorm:F3} → totalKg = {totalKg:F2} kg");
        }
    }

    private float ReadAxis(Joystick stick, string name)
    {
        var ctrl = stick.TryGetChildControl<AxisControl>(name);
        return ctrl != null ? Mathf.Clamp01(ctrl.ReadValue()) : 0f;
    }
}
