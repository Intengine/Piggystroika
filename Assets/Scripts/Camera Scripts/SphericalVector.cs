using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SphericalVector
{
    public float Length;
    public float Zenith;
    public float Azimuth;

    public SphericalVector(float length, float zenith, float azimuth)
    {
        Length = length;
        Zenith = zenith;
        Azimuth = azimuth;
    }

    public Vector3 Direction
    {
        get
        {
            Vector3 direction;

            float verticalAngle = Zenith * Mathf.PI / 2f;
            direction.y = Mathf.Sin(verticalAngle);
            float h = Mathf.Cos(verticalAngle);

            float horizontalAngle = Azimuth * Mathf.PI / 2f;
            direction.x = h * Mathf.Sin(horizontalAngle);
            direction.z = h * Mathf.Cos(horizontalAngle);

            return direction;
        }
    }

    public Vector3 Position
    {
        get
        {
            return Length * Direction;
        }
    }
}