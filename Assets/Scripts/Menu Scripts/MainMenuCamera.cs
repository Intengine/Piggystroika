using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public GameObject gameStartedPosition;
    public GameObject characterSelectPosition;

    private List<GameObject> positions = new List<GameObject>();

    private void Awake()
    {
        positions.Add(gameStartedPosition);
    }

    void Update()
    {
        MoveToPosition();
    }

    void MoveToPosition()
    {
        if(positions.Count > 0)
        {
            transform.position = Vector3.Lerp(transform.position, positions[0].transform.position, 1f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, positions[0].transform.rotation, 1f * Time.deltaTime);
        }
    }

    public void ChangePosition(int index)
    {
        positions.RemoveAt(0);

        if(index == 0)
        {
            positions.Add(gameStartedPosition);
        }
        else
        {
            positions.Add(characterSelectPosition);
        }
    }
}