using UnityEngine;

public class XRSimulatorSwitcher : MonoBehaviour
{
    public GameObject xrDeviceSimulator; // El objeto que contiene el XR Device Simulator
    public GameObject leftHand;          // El controlador izquierdo real
    public GameObject rightHand;         // El controlador derecho real

    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // En build para Quest → activar controladores reales, desactivar simulador
        if (xrDeviceSimulator != null) xrDeviceSimulator.SetActive(false);
        if (leftHand != null) leftHand.SetActive(true);
        if (rightHand != null) rightHand.SetActive(true);
#else
        // En editor de Unity (Windows) → activar simulador, desactivar controladores reales
        if (xrDeviceSimulator != null) xrDeviceSimulator.SetActive(true);
        if (leftHand != null) leftHand.SetActive(false);
        if (rightHand != null) rightHand.SetActive(false);
#endif
    }
}
