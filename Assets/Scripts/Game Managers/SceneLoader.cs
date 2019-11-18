using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [SerializeField]
    private GameObject loadingScreen;

    private string levelName;

    void Start()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(string name)
    {
        levelName = name;
        StartCoroutine(LoadLevelWithName());
    }

    IEnumerator LoadLevelWithName()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(levelName);
        yield return new WaitForSeconds(2.5f);
        loadingScreen.SetActive(false);

    }
}