using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMenu : MonoBehaviour
{
    
    public GameObject controlMenuUI, pauseMenuUI;
    private CharacterMenu charMenu;

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        GameManager.instance.CharacterMenu.UpdateMenu();
        Time.timeScale = 0f;
        CharacterMenu.GameIsPaused = true;
    }
    public void ControlMenuUI()
    {
        controlMenuUI.SetActive(true);
        Time.timeScale = 0f;
        CharacterMenu.GameIsPaused = true;
    }
}
