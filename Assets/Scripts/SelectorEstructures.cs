using UnityEngine;

public class SelectorEstructures : MonoBehaviour
{
    public GameObject pilar;
    public GameObject torreDos;
    public GameObject tres;

    public void ActivaPilar()
    {
        DesactivaTotes();
        if (pilar) pilar.SetActive(true);
    }

    public void ActivaTorreDos()
    {
        DesactivaTotes();
        if (torreDos) torreDos.SetActive(true);
    }

    public void ActivaTres()
    {
        DesactivaTotes();
        if (tres) tres.SetActive(true);
    }

    void DesactivaTotes()
    {
        if (pilar) pilar.SetActive(false);
        if (torreDos) torreDos.SetActive(false);
        if (tres) tres.SetActive(false);
    }
}
