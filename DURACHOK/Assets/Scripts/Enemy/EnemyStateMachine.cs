using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State { Patrol, Chase, Absorb }

    public State currentState;
    public List<Transform> patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 0.5f;
    public float sightRange = 5f;
    public float fieldOfViewAngle = 110f;
    public float rotationSpeed = 5f;
    public float pullSpeed = 2f;
    public float deathMenuDelay = 1.5f;

    private Transform target;
    private DurachokAbsorption durachokScript;
    private Collider durachokCollider;
    private int currentPatrolIndex = 0;
    private Vector3 originalScale;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Durachok").transform;
        durachokScript = target.GetComponent<DurachokAbsorption>();
        durachokCollider = target.GetComponent<Collider>();
        originalScale = target.localScale;
        currentState = State.Patrol;
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
        MoveTowards(currentPoint.position, patrolSpeed);

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
        if (durachokScript.IsInvisible())
        {
            currentState = State.Patrol;
            return;
        }

        MoveTowards(target.position, chaseSpeed);

        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            durachokScript.enabled = false;
            currentState = State.Absorb;
        }
    }

    IEnumerator AbsorbDurachok()
    {
        if (durachokCollider != null) durachokCollider.enabled = false;

        Vector3 startScale = target.localScale;
        Vector3 endScale = startScale * 0.5f;
        Vector3 targetPosition = transform.position;

        float time = 0f;
        while (time < 1f)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, time);
            target.position = Vector3.Lerp(target.position, targetPosition, time);
            time += Time.deltaTime * pullSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(deathMenuDelay);

        TeleportPlayerAndDurachok();

        currentState = State.Patrol;
        StartCoroutine(RestoreDurachok());
    }

    private void TeleportPlayerAndDurachok()
    {
        Vector3 playerNewPosition = CheckpointManager.lastCheckpointPosition;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;
            player.transform.position = playerNewPosition;
            if (controller != null) controller.enabled = true;
        }

        if (target != null)
        {
            target.position = playerNewPosition + new Vector3(1, 0, 0);
            Debug.Log("Durachok teleported near the player.");
        }

        UIManager.instance.EnabmeDeathMenu();
    }

    IEnumerator RestoreDurachok()
    {
        float time = 0f;
        while (time < 1f)
        {
            target.localScale = Vector3.Lerp(target.localScale, originalScale, time);
            time += Time.deltaTime;
            yield return null;
        }

        if (durachokCollider != null) durachokCollider.enabled = true;
        durachokScript.enabled = true;
    }

    bool CanSeeDurachok()
    {
        Vector3 directionToDurachok = target.position - transform.position;
        if (directionToDurachok.magnitude > sightRange) return false;

        float angleToDurachok = Vector3.Angle(transform.forward, directionToDurachok);
        if (angleToDurachok > fieldOfViewAngle / 2) return false;

        if (Physics.Raycast(transform.position, directionToDurachok.normalized, out RaycastHit hit, sightRange))
        {
            return hit.transform == target;
        }

        return false;
    }

    void MoveTowards(Vector3 destination, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        Vector3 direction = destination - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }
}