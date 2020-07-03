using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITank : Tank
{
    public NavMeshAgent agent { get { return GetComponent<NavMeshAgent>();}}

    public Animator fsm { get { return GetComponent<Animator>(); } }
    public Transform target;

    public Transform[] wayPoints;
    Vector3[] wayPointsPositions;
    int index;

    private void Start()
    {
        wayPointsPositions = new Vector3[wayPoints.Length];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPointsPositions[i] = wayPoints[i].position;
        }
    }
    protected override void Move()
    {
        //agent.SetDestination(other.position);
        float distance = Vector3.Distance(transform.position, other.position);
        fsm.SetFloat("distance", distance);

        float distanceFromWaypoint = Vector3.Distance(transform.position, wayPointsPositions[index]);
        fsm.SetFloat("distanceFromCurrentWaypoint", distanceFromWaypoint);

    }
    float delayed;
    internal void Shoot()
    {
        if ((delayed += Time.deltaTime) > 1f)
        {
            Fire();
            delayed = 0;
        }

    }
    public void LookAt()
    {
        LookAt(other);
    }
    protected override IEnumerator LookAt(Transform other)
    {
        while (Vector3.Angle(turret.forward, other.position - transform.position) > 5f)
        {
            turret.Rotate(turret.up, 4f);
            yield return null;
        }
    }


    internal void Patrol()
    {
        agent.SetDestination(wayPointsPositions[index]);
    }

    internal void Chase()
    {
        agent.SetDestination(other.position); 
    }

    public void FindNewWayPoint()
    {
        switch (index)
        {
            case 0:
                index = 1;
                break;
            case 1:
                index = 0;
                break;
        }   
        agent.SetDestination(wayPointsPositions[index]);
    }
}
