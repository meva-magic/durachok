using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera target;
    [SerializeField] private bool canLookVertically;


    private void Update()
    {
        if(canLookVertically)
        {
            transform.LookAt(target.transform);
        }

        else
        {
            Vector3 modifiedTarget = target.transform.position;
            modifiedTarget.y = target.transform.position.y;
            transform.LookAt(modifiedTarget);
        }
    }
}
