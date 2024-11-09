using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Attack
    }

    public State currentState;
    private Transform target; 
    public List<Transform> patrolPoints; 
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 1.5f;
    public float sightRange = 5f;
    public float fieldOfViewAngle = 110f;
    public float rotationSpeed = 5f; 
    public LayerMask playerLayer;

    public float pullSpeed = 2f; 
    public float pullThreshold = 1f; 

    private DurachokAbsorption durachokScript; 
    private int currentPatrolIndex = 0; 

    void Start()
    {
        currentState = State.Patrol;
        target = GameObject.FindGameObjectWithTag("Durachok").transform; 
        durachokScript = target.GetComponent<DurachokAbsorption>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Count == 0) return; 

      
        Transform currentPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, patrolSpeed * Time.deltaTime);


        if (transform.position != currentPoint.position)
        {
            Vector3 directionToMove = currentPoint.position - transform.position;
            directionToMove.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToMove), rotationSpeed * Time.deltaTime);
        }


        if (Vector3.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count; 
        }

        if (CanSeeDurachok() && !durachokScript.IsInvisible())
        {
            currentState = State.Chase;
        }
    }

    void Chase()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);

        Vector3 directionToDurachok = target.position - transform.position;
        directionToDurachok.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToDurachok), rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < pullThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, pullSpeed * Time.deltaTime);
            currentState = State.Attack;
        }

        if (!CanSeeDurachok() || durachokScript.IsInvisible())
        {
            currentState = State.Patrol;
        }
    }

    void Attack()
    {
            Destroy(target.gameObject);
            Debug.Log("Durachok is destroyed!");
    }

    bool CanSeeDurachok()
    {
        Vector3 directionToDurachok = target.position - transform.position;
        float distanceToDurachok = directionToDurachok.magnitude;

        if (distanceToDurachok <= sightRange)
        {
            float angleToDurachok = Vector3.Angle(transform.forward, directionToDurachok);

            if (angleToDurachok <= fieldOfViewAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToDurachok.normalized, out hit, sightRange, playerLayer))
                {
                    if (hit.transform == target)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
