using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherWorlds : MonoBehaviour
{
    public GameObject loadButton;

    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            loadButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Player")
        {
            loadButton.SetActive(false);
        }
    }
}