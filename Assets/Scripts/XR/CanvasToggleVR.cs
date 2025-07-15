using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CanvasToggleVR : MonoBehaviour
{
    [Header("Input Action")]
    public InputActionReference toggleAction;

    [Header("Canvas to Toggle")]
    public GameObject canvasObject;

    private void OnEnable()
    {
        if (toggleAction != null)
        {
            toggleAction.action.performed += ToggleCanvas;
            toggleAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (toggleAction != null)
        {
            toggleAction.action.performed -= ToggleCanvas;
            toggleAction.action.Disable();
        }
    }

    private void ToggleCanvas(InputAction.CallbackContext context)
    {
        if (canvasObject != null)
        {
            canvasObject.SetActive(!canvasObject.activeSelf);
        }
    }
}
