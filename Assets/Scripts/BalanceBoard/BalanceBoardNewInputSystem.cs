using UnityEngine;
using UnityEngine.InputSystem;

public class BalanceBoardCalibrated : MonoBehaviour
{
    [Header("Parámetros de dead-zone y offset")]
    public float deadZone = 0.1f;      // Ruido bajo este umbral será cero
    public Vector2 offset = Vector2.zero; // Lee este valor de “Set current balance as center”

    void Update()
    {
        if (Joystick.all.Count == 0) return;
        var stick = Joystick.all[0];

        float rawX = stick.stick.x.ReadValue();
        float rawY = stick.stick.y.ReadValue();

        // Aplicar offset
        float x = rawX - offset.x;
        float y = rawY - offset.y;

        // Dead-zone
        x = Mathf.Abs(x) < deadZone ? 0 : x;
        y = Mathf.Abs(y) < deadZone ? 0 : y;

        Vector2 cop = new Vector2(x, y);
        float intensidad = cop.magnitude;

        Debug.Log($"CoP calibrado: {cop}   Intensidad: {intensidad:F2}");
    }
}
