using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
   public bool hit=false;
   public bool canMove=true;

   private MeshRenderer mr;

   void Start()
   {
    mr=GetComponent<MeshRenderer>();
   }

   public void Dead()
   {
        Debug.Log("DEAD");
   }

   void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sword"))
        {
            if(!hit && GameManager.Instance.canSwing)
                Dead();
                UpdateManagers();
                mr.material.color=Color.grey;
                hit=true;
                canMove=false;
        }
    }

    private void UpdateManagers()
    {
        GameManager.Instance.ProgressValue+=1/(float)GameManager.Instance.EnemyCounter;
        UIManager.Instance.SetProgressbar(GameManager.Instance.ProgressValue);
    }
}
