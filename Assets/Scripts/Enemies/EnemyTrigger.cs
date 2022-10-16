using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyTrigger : MonoBehaviour
{
   public bool hit=false;
   public bool canMove=true;

   [SerializeField] private ParticleSystem deadZone;

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
            //&& GameManager.Instance.swinging
            if(!hit)
            {
                Dead();
                UpdateManagers();
                GetComponent<Animator>().enabled=false;
                //mr.material.color=Color.grey;
                hit=true;
                canMove=false;
                enemyRagdoll.ActiveRagdoll();
                PlayDeadZone();
                limbs[randomNumber].GetHit();

                StartCoroutine(DeactiveRagdoll());

            }
                
        }
    }

    private void UpdateManagers()
    {
        GameManager.Instance.ProgressValue+=1/(float)GameManager.Instance.EnemyCounter;
        UIManager.Instance.SetProgressbar(GameManager.Instance.ProgressValue);
    }

    IEnumerator DeactiveRagdoll()
    {
        yield return new WaitForSeconds(2f);
        enemyRagdoll.DeactiveRagdoll();
        gameObject.transform.DOScale(new Vector3(0.01f,0.01f,0.01f),2).OnComplete(()=>Destroy(gameObject));
        //Destroy(gameObject);
    }

    private void PlayDeadZone()
    {
        deadZone.Play();
        SoundManager.Instance.Play("magic");
    }
}
