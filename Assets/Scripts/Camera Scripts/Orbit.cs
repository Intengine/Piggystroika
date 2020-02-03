using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public SphericalVector sphericalVectorData = new SphericalVector(0, 0, 1);

    protected virtual void Update()
    {
        transform.position = sphericalVectorData.Position;
    }
}