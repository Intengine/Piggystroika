using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeopleWalk : MonoBehaviour
{
    public Transform[] walkPoints;
    public float walkSpeed = 1f;
    public bool isIdle;

    private int walkIndex;
    private NavMeshAgent navAgent;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if(isIdle)
        {
            animator.Play("Idle");
        }
        else
        {
            animator.Play("Walk");
        }
    }

    void Update()
    {
        if(!isIdle)
        {
            ChooseWalkPoint();
        }
    }

    void ChooseWalkPoint()
    {
        if(navAgent.remainingDistance <= 0.1f)
        {
            navAgent.SetDestination(walkPoints[walkIndex].position);
        } else if(walkIndex == walkPoints.Length - 1)
        {
            walkIndex = 0;
        }
        else
        {
            walkIndex++;
        }
    }
}