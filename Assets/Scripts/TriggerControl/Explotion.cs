using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{

    public float radius,force;
    private void OnTriggerEnter(Collider collider)
    {
        Vector3 explotionPos=transform.position;
        Collider[] colliders=Physics.OverlapSphere(explotionPos,radius);

        foreach(Collider collider1 in colliders)
        {
            Rigidbody rb=collider.GetComponent<Rigidbody>();

            if(rb!=null)
            {
                rb.AddExplosionForce(force,explotionPos,radius,0.05f,ForceMode.Force);
            }
        }
    }
}
