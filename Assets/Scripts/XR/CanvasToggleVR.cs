using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasToggleVRMulti : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference toggleCanvasY;
    public InputActionReference toggleCanvasX;

    [Header("Canvas Objects")]
    public GameObject canvasY;
    public GameObject canvasX;

    private void OnEnable()
    {
        if (toggleCanvasY != null)
        {
            toggleCanvasY.action.performed += ctx => ToggleCanvas(canvasY);
            toggleCanvasY.action.Enable();
        }

        if (toggleCanvasX != null)
        {
            toggleCanvasX.action.performed += ctx => ToggleCanvas(canvasX);
            toggleCanvasX.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (toggleCanvasY != null)
        {
            toggleCanvasY.action.performed -= ctx => ToggleCanvas(canvasY);
            toggleCanvasY.action.Disable();
        }

        if (toggleCanvasX != null)
        {
            toggleCanvasX.action.performed -= ctx => ToggleCanvas(canvasX);
            toggleCanvasX.action.Disable();
        }
    }

    private void ToggleCanvas(GameObject canvas)
    {
        if (canvas != null)
            canvas.SetActive(!canvas.activeSelf);
    }
}
