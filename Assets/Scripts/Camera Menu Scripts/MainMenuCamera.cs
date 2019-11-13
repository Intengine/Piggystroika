using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public GameObject gameStartedPosition;
    public GameObject characterSelectPosition;

    private bool reachedGameStartedPosition;
    private bool reachedCharacterSelectPosition = true;
    private bool canClick;
    private bool backToMainMenu;

    void Update()
    {
        MoveToGameStartedPosition();
        MoveToCharacterSelectMenu();
    }

    void MoveToGameStartedPosition()
    {
        if(!reachedGameStartedPosition)
        {
            if(Vector3.Distance(transform.position, gameStartedPosition.transform.position) < 0.2f) {
                reachedGameStartedPosition = true;
                canClick = true;
            }
        } else if(!reachedGameStartedPosition)
        {
            transform.position = Vector3.Lerp(transform.position, gameStartedPosition.transform.position, 1f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, gameStartedPosition.transform.rotation, 1f * Time.deltaTime);
        }
    }

    void MoveToCharacterSelectMenu()
    {
        if(!reachedCharacterSelectPosition)
        {
            transform.position = Vector3.Lerp(transform.position, characterSelectPosition.transform.position, 1f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, characterSelectPosition.transform.rotation, 1f * Time.deltaTime);
        } else if(!reachedCharacterSelectPosition)
        {
            if(Vector3.Distance(transform.position, characterSelectPosition.transform.position) < 0.2f)
            {
                reachedCharacterSelectPosition = true;
                canClick = true;
            }
        }
    }

    public bool ReachedCharacterSelectPosition
    {
        get { return reachedCharacterSelectPosition; }
        set { reachedCharacterSelectPosition = value; }
    }

    public bool CanClick
    {
        get { return canClick; }
        set { canClick = value; }
    }

    public bool BackToMainMenu
    {
        get { return BackToMainMenu; }
        set { backToMainMenu = value; }
    }
}