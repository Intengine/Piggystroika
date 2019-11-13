using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject buttonPanel, characterSelectPanel;

    private MainMenuCamera mainMenuCamera;

    void Awake()
    {
        mainMenuCamera = Camera.main.GetComponent<MainMenuCamera>();
    }

    public void PlayGame()
    {
        if(mainMenuCamera.CanClick)
        {
            mainMenuCamera.CanClick = false;
            buttonPanel.SetActive(false);
            characterSelectPanel.SetActive(true);
            mainMenuCamera.ReachedCharacterSelectPosition = false;
        }
    }

    public void BackToMainMenu()
    {
        if(mainMenuCamera.CanClick)
        {
            mainMenuCamera.CanClick = false;
            mainMenuCamera.BackToMainMenu = true;
            buttonPanel.SetActive(true);
            characterSelectPanel.SetActive(false);
        }
    }
}