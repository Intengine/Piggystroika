using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;

    void Start()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}