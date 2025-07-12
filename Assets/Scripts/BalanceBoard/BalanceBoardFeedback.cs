using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BalanceBoardFeedbackKidFriendly : MonoBehaviour
{
    [Header("Ventana de promedio (segundos)")]
    [Tooltip("Cuántos segundos miramos hacia atrás para calcular")]
    public float windowTime = 5f;

    [Header("Referencias UI (TMP)")]
    public TextMeshProUGUI lrBalanceText;
    public TextMeshProUGUI apBalanceText;
    public TextMeshProUGUI stabilityText;
    public TextMeshProUGUI recommendationText;

    private struct Sample { public Vector2 cop; public float time; }
    private Queue<Sample> samples = new Queue<Sample>();

    void Update()
    {
        if (Joystick.all.Count == 0) return;
        var stick = Joystick.all[0];

        // Leemos el punto de presión
        float x = stick.stick.x.ReadValue();
        float y = stick.stick.y.ReadValue();
        float now = Time.time;
        samples.Enqueue(new Sample { cop = new Vector2(x, y), time = now });

        // Quitamos muestras viejas
        while (samples.Count > 0 && now - samples.Peek().time > windowTime)
            samples.Dequeue();

        // Calculamos promedios y movimiento
        Vector2 sum = Vector2.zero;
        float path = 0f;
        Vector2 prev = Vector2.zero;
        bool first = true;
        foreach (var s in samples)
        {
            sum += s.cop;
            if (!first) path += Vector2.Distance(prev, s.cop);
            prev = s.cop;
            first = false;
        }
        int n = samples.Count;
        if (n == 0) return;

        // Porcentajes de desvío
        float avgX = (sum.x / n) * 100f;   // % izquierda/derecha
        float avgY = (sum.y / n) * 100f;   // % adelante/atrás
        float swaySpeed = path / windowTime; // cuánto se mueve

        // Mostramos en pantalla
        lrBalanceText.text = $"Izq/Der: {avgX:+0.0;-0.0;0}%";
        apBalanceText.text = $"Adel/Atrás: {avgY:+0.0;-0.0;0}%";
        stabilityText.text = $"Movimiento: {swaySpeed:0.00}/s";

        // Recomendaciones sencillas
        string rec = "";
        // Centro de gravedad centrado
        rec += "– Mantén el peso lo más centrado posible.\n";
        // Fuerza en puntas de los pies
        rec += "– Empuja con las puntas de los pies, no con los talones.\n";
        // Correcciones de desvío
        if (avgX > 5f) rec += "– Te vas a la derecha, ve un poco al centro.\n";
        else if (avgX < -5f) rec += "– Te vas a la izquierda, ve un poco al centro.\n";

        if (avgY > 5f) rec += "– Te inclinas hacia adelante, enderézate.\n";
        else if (avgY < -5f) rec += "– Te inclinas hacia atrás, inclínate un poco al frente.\n";

        // Mantener posición
        if (swaySpeed > 0.2f) rec += "– Procura quedarte más quieto en la misma posición.\n";

        if (string.IsNullOrEmpty(rec))
            rec = "✅ ¡Estás perfecto! Sigue así, centrado y empujando con las puntas.";

        recommendationText.text = rec;
    }
}
