using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{

    List<Rigidbody> ragdollRigidbody;
    void Start()
    {
        ragdollRigidbody=new List<Rigidbody>(transform.GetComponentsInChildren<Rigidbody>());
        ragdollRigidbody.Remove(GetComponent<Rigidbody>());


        DeactiveRagdoll();
    }


    public void ActiveRagdoll()
    {
        //Animasyonu enabled yapma
        for(int i=0; i<ragdollRigidbody.Count; i++)
        {
            ragdollRigidbody[i].useGravity=true;
            ragdollRigidbody[i].isKinematic=false;
        }
    }

    public void DeactiveRagdoll()
    {
        //Animasyonu enabled yap.
        for(int i=0; i<ragdollRigidbody.Count; i++)
        {

            ragdollRigidbody[i].useGravity=false;
            ragdollRigidbody[i].isKinematic=true;

            //new
            
            //ragdollRigidbody[i].GetComponent<Collider>().isTrigger=true;

        }
    }

    
}
