using UnityEngine;
using System.Collections.Generic;

public class CamisaColorGlobalManager : MonoBehaviour
{
    [Header("Materials a canviar de color")]
    [Tooltip("Llista de materials compartits (des del Project) que canvien de color")]
    public List<Material> materialsACanviar;

    /// <summary>
    /// Aplica el color a tots els materials de la llista
    /// </summary>
    public void CanviarColorCamises(Color color)
    {
        if (materialsACanviar == null || materialsACanviar.Count == 0)
        {
            Debug.LogWarning("⚠️ No s'ha assignat cap material a la llista 'materialsACanviar'");
            return;
        }

        foreach (var mat in materialsACanviar)
        {
            if (mat != null)
            {
                mat.color = color;
                Debug.Log($"🎨 Color aplicat a '{mat.name}': {color}");
            }
        }
    }
}
