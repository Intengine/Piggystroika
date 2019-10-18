using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : Orbit
{
    public Vector3 targetOffset = new Vector3(0, 2, 0);
    public Vector3 cameraPositionZoom = new Vector3(-0.5f, 0, 0);
    public float cameraLength = -10f;
    public float cameraLengthZoom = -5f;
    public Vector2 orbitSpeed = new Vector2(0.01f, 0.01f);
    public Vector2 orbitOffset = new Vector2(0, -0.8f);
    public Vector2 angleOffset = new Vector2(0, -0.25f);

    private float zoomValue;
    private Vector3 cameraPositionTemporary;
    private Vector3 cameraPosition;

    private Transform playerTarget;
    private Camera mainCamera;

    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        sphericalVectorData.Length = cameraLength;
    }

    void Update()
    {
        
    }
}