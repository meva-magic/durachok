using UnityEngine;

public class LowerOnProximity : MonoBehaviour
{
    public string playerTag = "Player";
    public string durachokTag = "Durachok";
    public float requiredDistance = 5f;  // Distance to trigger lowering
    public float lowerDistance = 2f;     // Distance to lower the object
    public float loweringSpeed = 2f;     // Speed of lowering

    private Vector3 originalPosition;
    private bool isLowering = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        GameObject player = GameObject.FindWithTag(playerTag);
        GameObject durachok = GameObject.FindWithTag(durachokTag);

        if (player && durachok)
        {
            float playerDistance = Vector3.Distance(player.transform.position, transform.position);
            float durachokDistance = Vector3.Distance(durachok.transform.position, transform.position);

            // Check if both are within the required distance
            if (playerDistance <= requiredDistance && durachokDistance <= requiredDistance)
            {
                isLowering = true;
            }
            else
            {
                isLowering = false;
            }

            // Move object if conditions are met
            if (isLowering)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition - new Vector3(0, lowerDistance, 0), loweringSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, loweringSpeed * Time.deltaTime);
            }
        }
    }
}
