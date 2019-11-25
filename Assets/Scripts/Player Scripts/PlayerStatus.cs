using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameObject[] playerSwords;
    private GameObject itemsPanel;

    void Start()
    {
        itemsPanel = GameObject.Find("Items Panel");
        itemsPanel.SetActive(false);

        GameObject.Find("Items Button").GetComponent<Button>().onClick.AddListener(ActivateItemsPanel);
    }

    public void ActivateItemsPanel()
    {
        if(itemsPanel.activeInHierarchy)
        {
            itemsPanel.SetActive(false);
        }
        else
        {
            itemsPanel.SetActive(true);
        }
    }
}