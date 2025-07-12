using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CollaSelectorColor : MonoBehaviour
{
    [System.Serializable]
    public class Colla
    {
        public string nom;
        public Color colorCamisa;
    }

    [Header("Dades de les colles")]
    public Colla[] colles;

    [Header("Referències UI")]
    public Dropdown collaDropdown;
    public SkinnedMeshRenderer rendererCamisa;
    public int indexMaterialCamisa = 0;

    public CamisaColorGlobalManager camisaManager;

    [Header("UI Personalització")]
    public TMP_InputField inputMalNom;
    public Dropdown posicioDropdown;
    public Button[] botonsPersonalitzables;

    private const string PREF_KEY = "COLLA_SELECCIONADA";

    void Start()
    {
        // Omplir colla dropdown
        collaDropdown.ClearOptions();
        foreach (var colla in colles)
            collaDropdown.options.Add(new Dropdown.OptionData(colla.nom));

        // Omplir posicions
        posicioDropdown.ClearOptions();
        posicioDropdown.AddOptions(new System.Collections.Generic.List<string> {
            "Baix", "Segon", "Aixecador", "Enxaneta", "Crossa", "Lateral"
        });

        int savedIndex = PlayerPrefs.GetInt(PREF_KEY, 0);
        collaDropdown.value = savedIndex;
        collaDropdown.onValueChanged.AddListener(AplicaColorPerIndex);
        AplicaColorPerIndex(savedIndex);

        inputMalNom.text = PlayerPrefs.GetString("MALNOM", "");
        posicioDropdown.value = PlayerPrefs.GetInt("POSICIO_PREF", 0);

        inputMalNom.onEndEdit.AddListener((text) => PlayerPrefs.SetString("MALNOM", text));
        posicioDropdown.onValueChanged.AddListener((i) => PlayerPrefs.SetInt("POSICIO_PREF", i));
    }

    public void AplicaColorPerIndex(int index)
    {
        if (index < 0 || index >= colles.Length) return;

        Color color = colles[index].colorCamisa;

        // Canviar camisa local
        if (rendererCamisa != null)
        {
            Material[] mats = rendererCamisa.materials;
            if (indexMaterialCamisa < mats.Length)
            {
                mats[indexMaterialCamisa].color = color;
                rendererCamisa.materials = mats;
            }
        }

        // Aplicar a tots els castellers
        if (camisaManager != null)
            camisaManager.CanviarColorCamises(color);

        // Aplicar color als botons
        foreach (var btn in botonsPersonalitzables)
        {
            var colors = btn.colors;
            colors.normalColor = color;
            colors.highlightedColor = color * 1.1f;
            colors.pressedColor = color * 0.9f;
            btn.colors = colors;
        }

        PlayerPrefs.SetInt(PREF_KEY, index);
    }
}
