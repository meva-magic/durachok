using UnityEngine;

public class SwordPickUp : MonoBehaviour
{
    [SerializeField] GameObject pickUpEffect;

    //public Sword swordScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player;
    public float pickUpRange;

    void PickUp()
    {
        //swordScript.enabled = true;
        AudioManager.instance.Play("SwordSwoosh");
        Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        //UIManager.instance.GetSword();
        Destroy(gameObject);
    }

    private void Start()
    {
        //swordScript.enabled = false;
    }

    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            PickUp();
        }
    }
}
