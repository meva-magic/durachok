using UnityEngine;
using System.Collections;

public class DurachokAbsorption : MonoBehaviour
{
    public GameObject timer;
    public Transform player;               
    public float arcHeight = 2f;           
    public float invisibleDuration = 5f;
    public float timeLeft;
    private Vector3 originalScale;         
    private Collider durachokCollider;    

    public bool isInvisible = false;       

    public float respawnRadius = 2f;     

    public static DurachokAbsorption instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalScale = transform.localScale;
        durachokCollider = GetComponent<Collider>();
    }

    public IEnumerator AbsorbIntoPlayer()
    {
        if (durachokCollider != null)
        {
            durachokCollider.enabled = false;
        }

        Vector3 startPosition = transform.position;
        Vector3 endPosition = player.position;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero; 

        float time = 0f;
        while (time < 1f)
        {
            Vector3 arcPosition = Vector3.Lerp(startPosition, endPosition, time);
            arcPosition.y += Mathf.Sin(time * Mathf.PI) * arcHeight;
            transform.position = arcPosition;

            transform.localScale = Vector3.Lerp(startScale, endScale, time);
            time += Time.deltaTime;
            yield return null;
        }

        timer.SetActive(true);
        timeLeft = invisibleDuration;
        isInvisible = true;
        transform.localScale = endScale;

        float invisibleTime = 0f;
        while (invisibleTime < invisibleDuration)
        {
            transform.position = player.position;
            invisibleTime += Time.deltaTime;
            timeLeft = invisibleDuration - invisibleTime;
            yield return null;
        }

       
        Vector3 randomOffset = new Vector3(Random.Range(-respawnRadius, respawnRadius), 0f, Random.Range(-respawnRadius, respawnRadius));
        Vector3 randomPosition = player.position + randomOffset;

       
        time = 0f;
        Vector3 exitPosition = randomPosition; 

        while (time < 1f)
        {
            Vector3 arcPosition = Vector3.Lerp(exitPosition, randomPosition, time);
            arcPosition.y += Mathf.Sin(time * Mathf.PI) * arcHeight;
            transform.position = arcPosition;

            transform.localScale = Vector3.Lerp(endScale, originalScale, time);
            time += Time.deltaTime;
            yield return null;
        }

        if (durachokCollider != null)
        {
            durachokCollider.enabled = true;
        }

        timer.SetActive(true);
        isInvisible = false;
    }

    public bool IsInvisible()
    {
        return isInvisible;
    }
}
