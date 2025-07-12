using UnityEngine;

public class CamisaColorGlobalManager : MonoBehaviour
{
    [Header("Paràmetres")]
    [Tooltip("Nom del material de la camisa (sense instància)")]
    public string nomMaterialCamisa = "shirt_SHD2";

    /// <summary>
    /// Aplica el color a tots els materials que coincideixin amb el nom indicat
    /// </summary>
    public void CanviarColorCamises(Color color)
    {
        SkinnedMeshRenderer[] renderers = FindObjectsOfType<SkinnedMeshRenderer>();

        foreach (var rend in renderers)
        {
            Material[] materials = rend.materials;
            bool canviat = false;

            for (int i = 0; i < materials.Length; i++)
            {
                // Atenció: materials[i].name pot ser "shirt_SHD2 (Instance)"
                if (materials[i] != null && materials[i].name.Contains(nomMaterialCamisa))
                {
                    Material novaInstancia = new Material(materials[i]);
                    novaInstancia.color = color;
                    materials[i] = novaInstancia;
                    canviat = true;
                }
            }

            if (canviat)
            {
                rend.materials = materials;
            }
        }

        Debug.Log($"🎽 Color aplicat a totes les camises amb nom '{nomMaterialCamisa}'");
    }
}
