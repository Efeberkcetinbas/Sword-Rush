using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyTrigger : MonoBehaviour
{
   public bool hit=false;
   public bool canMove=true;
   public bool isDead=false;

   //[SerializeField] private ParticleSystem deadZone;

   [SerializeField] private SkinnedMeshRenderer smr;

   private BoxCollider boxCollider;


   public int randomNumber;

    [SerializeField] private EnemyRagdoll enemyRagdoll;
    [SerializeField] private List<LimbControl> limbs=new List<LimbControl>();

    private GameManager gameManager;
    private UIManager uiManager;
    private CameraManager cameraManager;
   void Start()
   {
        gameManager=GameManager.Instance;
        uiManager=UIManager.Instance;
        cameraManager=CameraManager.Instance;
        randomNumber=Random.Range(0,9);
        boxCollider=GetComponent<BoxCollider>();
   }

   public void Dead()
   {
        Debug.Log("DEAD");
        smr.material.color=Color.grey;
        boxCollider.enabled=false;
        UpdateManagers();
        GetComponent<Animator>().enabled=false;
        canMove=false;
        enemyRagdoll.ActiveRagdoll();
        //PlayDeadZone();
        limbs[randomNumber].GetHit();
        isDead=true;
        StartCoroutine(DeactiveRagdollCar());

        if(gameManager.ProgressValue==1f)
        {
            gameManager.BarFullPlayParticle();
            gameManager.IncreaseSwordAreaCollider();
            ResetProgressVal();
            //StartCoroutine(ResetProgressValue());
        }

   }

   private IEnumerator ResetProgressValue()
   {
        yield return new WaitForSeconds(2);
        gameManager.ProgressValue=0;
        UIManager.Instance.ProgressBar.DOFillAmount(0, .3f);
   }

    //Yukaridaki method ya da bu
   private void ResetProgressVal()
   {
        gameManager.canProgressContinue=false;
        gameManager.ProgressValue=0;
        UIManager.Instance.ProgressBar.DOFillAmount(0,.5f).OnComplete(()=>
        {
            gameManager.canProgressContinue=true;
        });
   }

   void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sword"))
        {
            //&& GameManager.Instance.swinging
            if(!hit && GameManager.Instance.canDoDamage)
            {
                hit=true;
                Dead();
            }
                
        }
        if(other.CompareTag("Player") && !gameManager.canDoDamage && !gameManager.isPlayerDead)
        {
            
            other.gameObject.GetComponent<PlayerTrigger>().HealthChange(1);
            uiManager.UpdateHealthBar((float)other.gameObject.GetComponent<PlayerTrigger>().currentHealth/(float)other.gameObject.GetComponent<PlayerTrigger>().maxHealth);
            uiManager.ColorChanger(uiManager.playerHealthImage,Color.green,Color.red,0.25f);

            if( other.gameObject.GetComponent<PlayerTrigger>().currentHealth<=0)
            {
                uiManager.UpdateHealthBar(0);
                Debug.Log("PLAYER DEAD ");
                gameManager.isPlayerDead=true;
                gameManager.isGameEnd=true;
                gameManager.Player.GetComponent<Animator>().SetBool("playerDead",true);
                
            }
        }
    }

    private void UpdateManagers()
    {
        if(gameManager.canProgressContinue)
        {
            gameManager.ProgressValue+=1/(float)gameManager.EnemyCounter;
            uiManager.SetProgressbar(gameManager.ProgressValue);
            uiManager.ColorChanger(uiManager.ProgressBar,Color.yellow,Color.red,0.1f);
        }
        
        cameraManager.ShakeIt();

    }

    

    IEnumerator DeactiveRagdollCar()
    {
        yield return new WaitForSeconds(2f);
        enemyRagdoll.DeactiveRagdoll();
        gameObject.transform.DOScale(new Vector3(0.01f,0.01f,0.01f),2).OnComplete(()=>Destroy(gameObject));
        //Destroy(gameObject);
    }

    /*private void PlayDeadZone()
    {
        deadZone.Play();
        SoundManager.Instance.Play("magic");
    }*/
}
