using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject characterPosition;

    private int pigIndex = 0;
    private int catIndex = 1;
    private int rabbitIndex = 2;

    void Start()
    {
        characters[pigIndex].SetActive(true);
        characters[pigIndex].transform.position = characterPosition.transform.position;
    }

    public void SelectCharacter()
    {
        int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        TurnOffCharacters();

        characters[index].SetActive(true);
        characters[index].transform.position = characterPosition.transform.position;
    }

    void TurnOffCharacters()
    {
        for(int i=0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
    }
}