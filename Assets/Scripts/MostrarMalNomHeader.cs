using UnityEngine;
using UnityEngine.UI;

public class MostrarMalNomHeader : MonoBehaviour
{
    public Text textHeader;

    void Start()
    {
        string malnom = PlayerPrefs.GetString("MALNOM", "");

        if (!string.IsNullOrEmpty(malnom))
            textHeader.text = $"Benvingut, {malnom}!";
        else
            textHeader.text = "Benvingut!";
    }
}
