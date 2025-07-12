using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BalanceBoardFull : MonoBehaviour
{
    void Update()
    {
        if (Joystick.all.Count == 0) return;
        var stick = Joystick.all[0];

        // Ejes X/Y (CoP)
        float x = stick.stick.x.ReadValue();
        float y = stick.stick.y.ReadValue();

        // Ejes extra (sensor TL, TR, BL, BR)
        var z = stick.TryGetChildControl<AxisControl>("z")?.ReadValue() ?? 0f;
        var rx = stick.TryGetChildControl<AxisControl>("rx")?.ReadValue() ?? 0f;
        var ry = stick.TryGetChildControl<AxisControl>("ry")?.ReadValue() ?? 0f;
        var rz = stick.TryGetChildControl<AxisControl>("rz")?.ReadValue() ?? 0f;

        // Opcional: convierte esos valores normalizados a kilogramos según tu tare/calentamiento
        float kgTL = MapSensor(z);
        float kgTR = MapSensor(rx);
        float kgBL = MapSensor(ry);
        float kgBR = MapSensor(rz);
        float totalKg = kgTL + kgTR + kgBL + kgBR;

        Debug.Log($"CoP: ({x:F2},{y:F2})  Sensors kg: TL={kgTL:F1},TR={kgTR:F1},BL={kgBL:F1},BR={kgBR:F1}  Total={totalKg:F1}kg");
    }

    float MapSensor(float normalized)
    {
        // Ajusta según la lectura en vacío (0) y carga máxima (~1)
        const float maxKg = 40f;  // por ejemplo
        return Mathf.Clamp(normalized, 0, 1) * maxKg;
    }
}
