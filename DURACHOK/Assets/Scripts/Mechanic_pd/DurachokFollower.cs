using UnityEngine;

public class DurachokFollower : MonoBehaviour
{
    public Transform player;  
    public float followDistance = 2f;   
    public float followSpeed = 2f;     
    public float levitateHeight = 1f;   
    public float activationRadius = 5f; 

    private bool isFollowing = false;   

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Input.GetKeyDown(KeyCode.Return) && distanceToPlayer <= activationRadius)
        {
            isFollowing = true;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            isFollowing = false;
        }

        if (isFollowing)
        {
            Vector3 targetPosition = player.position - player.forward * followDistance;
            targetPosition.y += levitateHeight;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
