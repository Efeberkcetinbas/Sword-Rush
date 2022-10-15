using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
   public bool hit=false;
   public bool canMove=true;

   //private MeshRenderer mr;

   public int randomNumber;

    [SerializeField] private EnemyRagdoll enemyRagdoll;
    [SerializeField] private List<LimbControl> limbs=new List<LimbControl>();
   void Start()
   {
        //mr=GetComponent<MeshRenderer>();
        randomNumber=Random.Range(0,9);
   }

   public void Dead()
   {
        Debug.Log("DEAD");
   }

   void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sword"))
        {
            if(!hit && GameManager.Instance.swinging)
            {
                Dead();
                UpdateManagers();
                //mr.material.color=Color.grey;
                hit=true;
                canMove=false;
                enemyRagdoll.ActiveRagdoll();

                limbs[randomNumber].GetHit();

            }
                
        }
    }

    private void UpdateManagers()
    {
        GameManager.Instance.ProgressValue+=1/(float)GameManager.Instance.EnemyCounter;
        UIManager.Instance.SetProgressbar(GameManager.Instance.ProgressValue);
    }
}
