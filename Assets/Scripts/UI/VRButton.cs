using UnityEngine;

public class VRButton : MonoBehaviour
{
    public enum ButtonAction
    {
        LoadProfile,
        LoadExplore,
        LoadTrain,
        LoadSoroll,
        LoadMainMenu,
        QuitGame
    }

    public ButtonAction action;

    public void OnPress()
    {
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui == null)
        {
            Debug.LogError("UIManager no trobat a l'escena.");
            return;
        }

        switch (action)
        {
            case ButtonAction.LoadProfile:
                ui.LoadProfile();
                break;
            case ButtonAction.LoadExplore:
                ui.LoadExplore();
                break;
            case ButtonAction.LoadTrain:
                ui.LoadTrain();
                break;
            case ButtonAction.LoadMainMenu:
                ui.LoadMainMenu();
                break;
            case ButtonAction.LoadSoroll:
                ui.LoadSoroll();
                break;

            case ButtonAction.QuitGame:
                ui.QuitGame();
                break;
        }
    }
}
