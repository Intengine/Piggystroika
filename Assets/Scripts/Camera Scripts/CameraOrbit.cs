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
        mainCamera = Camera.main;

        sphericalVectorData.Length = cameraLength;
        sphericalVectorData.Azimuth = angleOffset.x;
        sphericalVectorData.Zenith = angleOffset.y;

        cameraPositionTemporary = mainCamera.transform.localPosition;
        cameraPosition = cameraPositionTemporary;

        MouseLock.MouseLocked = true;
    }

    void Update()
    {
        HandleCamera();
        HandleMouseLocking();
    }

    void HandleCamera()
    {
        if(MouseLock.MouseLocked)
        {
            sphericalVectorData.Azimuth += Input.GetAxis("Mouse X") * orbitSpeed.x;
            sphericalVectorData.Zenith += Input.GetAxis("Mouse Y") * orbitSpeed.y;
        }

        sphericalVectorData.Zenith = Mathf.Clamp(sphericalVectorData.Zenith + orbitOffset.x, orbitOffset.y, 0f);

        float distanceToObject = zoomValue;
        float deltaDistance = Mathf.Clamp(zoomValue, distanceToObject, -distanceToObject);
        sphericalVectorData.Length += (deltaDistance - sphericalVectorData.Length);

        Vector3 lookAt = targetOffset;
        lookAt += playerTarget.position;

        base.Update();

        transform.position += lookAt;
        transform.LookAt(lookAt);

        if(zoomValue == cameraLengthZoom)
        {
            Quaternion targetRotation = transform.rotation;
            targetRotation.x = 0f;
            targetRotation.z = 0f;
            playerTarget.rotation = targetRotation;
        }
        cameraPosition = cameraPositionTemporary;
        zoomValue = cameraLength;
    }

    void HandleMouseLocking()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(MouseLock.MouseLocked)
            {
                MouseLock.MouseLocked = false;
            }
            else
            {
                MouseLock.MouseLocked = true;
            }
        }
    }
}