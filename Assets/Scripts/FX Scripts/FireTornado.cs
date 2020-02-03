using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornado : MonoBehaviour
{
    public float speed = 250f;
    public float maxSpeed = 350f;
    public float speedMultiplier = 1f;

    private float lifeTime = 4f;

    private Rigidbody myBody;

    private Transform player;
    private Vector3 direction;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = player.forward;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        speed += speedMultiplier;

        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        myBody.velocity = speed * Time.deltaTime * direction;
    }
}