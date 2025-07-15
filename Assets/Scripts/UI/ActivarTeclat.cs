using UnityEngine;
using TMPro;

public class ActivarTeclat : MonoBehaviour
{
    public GameObject teclatPrefab;
    public TMP_InputField inputField;

    public void ObrirTeclat()
    {
        teclatPrefab.SetActive(true);
        inputField.Select();
        inputField.ActivateInputField();
    }
}
