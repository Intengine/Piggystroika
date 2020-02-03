using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToVillage : MonoBehaviour
{
    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            SceneLoader.instance.LoadLevel("Village");
        }
    }
}