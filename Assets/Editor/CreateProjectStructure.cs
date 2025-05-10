using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateProjectStructure
{
    [MenuItem("Tools/Crear estructura CastellEmotion")]
    public static void CreateFolders()
    {
        string[] folders = new string[]
        {
            "Assets/Art",
            "Assets/Art/Materials",
            "Assets/Art/Models",
            "Assets/Art/Textures",
            "Assets/Audio",
            "Assets/Prefabs",
            "Assets/Scenes",
            "Assets/Scripts",
            "Assets/Scripts/Core",
            "Assets/Scripts/UI",
            "Assets/Scripts/Gameplay",
            "Assets/Scripts/Simulation",
            "Assets/Scripts/Utils",
            "Assets/Resources",
            "Assets/VR",
            "Assets/Tests"
        };

        foreach (string folder in folders)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                Debug.Log("Creada carpeta: " + folder);
            }
            else
            {
                Debug.Log("Ja existeix: " + folder);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("✅ Estructura de carpetes creada amb èxit!");
    }
}
