using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Absorb
    }

    public State currentState;
    private Transform target;
    public List<Transform> patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 0.5f;
    public float sightRange = 5f;
    public float fieldOfViewAngle = 110f;
    public float rotationSpeed = 5f;
    public LayerMask playerLayer;

    public float pullSpeed = 2f;
    public float pullThreshold = 1f;

    private DurachokAbsorption durachokScript;
    private int currentPatrolIndex = 0;
    private Collider durachokCollider;

    void Start()
    {
        currentState = State.Patrol;
        target = GameObject.FindGameObjectWithTag("Durachok").transform;
        durachokScript = target.GetComponent<DurachokAbsorption>();
        durachokCollider = target.GetComponent<Collider>();
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

            case State.Absorb:
                StartCoroutine(AbsorbDurachok());
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
        if (durachokScript.IsInvisible()) // Если Дурочек невидим, возвращаемся к патрулю
        {
            currentState = State.Patrol;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);

        Vector3 directionToDurachok = target.position - transform.position;
        directionToDurachok.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToDurachok), rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            durachokScript.enabled = false; // Отключаем способность следования за игроком
            currentState = State.Absorb;
        }
    }

    IEnumerator AbsorbDurachok()
    {
        // Отключаем коллайдер Дурочка и готовим его для поглощения
        if (durachokCollider != null)
        {
            durachokCollider.enabled = false;
        }

        Vector3 startScale = target.localScale;
        Vector3 endScale = Vector3.zero;

        float time = 0f;
        Vector3 targetPosition = transform.position;
        while (time < 1f)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, time);
            target.position = Vector3.Lerp(target.position, targetPosition, time);
            time += Time.deltaTime * pullSpeed;
            yield return null;
        }

        target.gameObject.SetActive(false);
        Debug.Log("Durachok absorbed! Respawn menu triggered.");
        UIManager.instance.EnabmeDeathMenu();

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
                // Используем Raycast с проверкой на наличие препятствий
                if (Physics.Raycast(transform.position, directionToDurachok.normalized, out hit, sightRange))
                {
                    // Проверяем, что именно Дурочек является первой пересеченной целью
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