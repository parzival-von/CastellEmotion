using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;

public class CreateProjectStructure
{
    [MenuItem("Tools/Crear estructura CastellEmotion")]
    public static void CreateFoldersAndScenes()
    {
        string[] folders = new string[]
        {
            "Assets/Art/Materials",
            "Assets/Art/Models",
            "Assets/Art/Textures",
            "Assets/Audio",
            "Assets/Prefabs",
            "Assets/Scenes",
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
                Debug.Log("📁 Creada carpeta: " + folder);
            }
        }

        AssetDatabase.Refresh();

        // Crear escenes
        string[] sceneNames = { "MainMenu", "Profile", "Explore", "Train" };
        string scenePath = "Assets/Scenes/";

        foreach (string name in sceneNames)
        {
            string fullPath = scenePath + name + ".unity";
            if (!File.Exists(fullPath))
            {
                var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(newScene, fullPath);
                Debug.Log("🎬 Escena creada: " + name);
            }
            else
            {
                Debug.Log("✔️ Escena ja existeix: " + name);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("✅ Estructura i escenes creades correctament.");
    }
}
