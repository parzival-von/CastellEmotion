using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarregarEscenaAmbBoto : MonoBehaviour
{
    [Header("Nom de l'escena a carregar")]
    public string nomEscena;

    [Header("Botó a vincular")]
    public Button boto;

    void Start()
    {
        if (boto != null)
        {
            boto.onClick.AddListener(CarregarEscena);
        }
    }

    public void CarregarEscena()
    {
        if (!string.IsNullOrEmpty(nomEscena))
        {
            SceneManager.LoadScene(nomEscena);
        }
        else
        {
            Debug.LogWarning("⚠️ No s'ha assignat cap nom d'escena!");
        }
    }
}
