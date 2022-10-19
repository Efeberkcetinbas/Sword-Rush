using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;
    public GameObject IncrementalPanel;


    [Header("Bools")]
    public bool isPlayerHide=false;
    public bool swinging=false;
    //canSwing kesme hareketine yariyor.
    public bool canSwing=true;
    public bool canDoDamage=false;

    [Header("Times and Values")]
    public float swingTime;
    public float incrementalSwingTime;
    public float radialSwingTime;
    public float damageTime;

    public int EnemyCounter;
    public int EarningMoney;
    public float increaseValue;
    public float ProgressValue;
    public float SwordArea;

    [Range(0,1.5f)]public float SelectDamageTime;

    [Header("Particles")]
    public ParticleSystem splashParticle;
    public ParticleSystem barfullParticle;




    public Vector3 PlayerInitPos;

    public Transform sword;

    public InteractUpgrade interactUpgrade;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        GetAllPlayerPrefs();
        sword.transform.localScale=new Vector3(0.2f+SwordArea,0.2f+SwordArea,0.4f+SwordArea);
    }

    private void Update()
    {
        //swinging=False
        if(canSwing)
        {
            swinging=false;
            swingTime+=Time.deltaTime;
            radialSwingTime-=Time.deltaTime;
            damageTime+=Time.deltaTime;
            canDoDamage=true;
            if(damageTime>SelectDamageTime)
                canDoDamage=false;
            
            if(swingTime>incrementalSwingTime)
            {
                canDoDamage=false;
                damageTime=0;
                radialSwingTime=incrementalSwingTime;
                //sword collider acip kapatma yapabilirsin burada
                swinging=true;
                swingTime=0;
                canSwing=false;
                //Hissiyat acisindan bu daha iyi oldu.
            }

            
        }

        UIManager.Instance.SetRadialProgressBar(radialSwingTime/incrementalSwingTime);
    }
   
    public void UpdateEnemyCounter()
    {
        EnemyCounter=FindObjectOfType<CountOfEnemy>().counter;
        increaseValue=1/(float)GameManager.Instance.EnemyCounter;
    }

    public void ResetTheLevel()
    {
        ProgressValue=0;
        UIManager.Instance.ProgressBar.DOFillAmount(0, .1f);
        Player.transform.position=PlayerInitPos;
    }

    public void SwordPlayParticle()
    {
        splashParticle.Play();
    }

    public void BarFullPlayParticle()
    {
        barfullParticle.Play();
    }

    public void IncreaseSwordAreaCollider()
    {
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        var val=sword.transform.localScale.x;
        Debug.Log("VAL : " + val);
        //Buyuk ihtimal buradan dolayi dotween hatasi aliyorum. Buraya bak !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        sword.transform.DOScale(new Vector3(val*20,val*20,val*20),1f).OnComplete(()=>sword.transform.DOScale(new Vector3(val,val,val+2),1f));
        
    }

    public void GetAllPlayerPrefs()
    {
        incrementalSwingTime=PlayerPrefs.GetFloat("incrementals",2);
        radialSwingTime=incrementalSwingTime;
        EarningMoney=PlayerPrefs.GetInt("earningMoney",10);
        SwordArea=PlayerPrefs.GetFloat("areaHit",0.1f);
    }

    #region Incremental

    public void CloseIncrementalPanel()
    {
        IncrementalPanel.transform.DOScale(Vector3.zero,0.25f).OnComplete(()=>IncrementalPanel.SetActive(false));
    }

    public void OpenIncrementalPanel()
    {
        IncrementalPanel.transform.DOScale(Vector3.one,0.25f).OnComplete(()=>IncrementalPanel.SetActive(true));
    }
    public void UpdateIncome()
    {
        EarningMoney*=2;
        PlayerPrefs.SetInt("earningMoney",EarningMoney);
        interactUpgrade.CheckButtonsInteraction();
        UIManager.Instance.UpdateEarn();
    }

    public void UpdateHitArea()
    {
        SwordArea=SwordArea+0.1f;
        Debug.Log("SWORD LOCAL SCALE :" + SwordArea);
        sword.transform.localScale=new Vector3(0.2f+SwordArea,0.2f+SwordArea,0.4f+SwordArea);
        PlayerPrefs.SetFloat("areaHit",SwordArea);
        interactUpgrade.CheckButtonsInteraction();
        UIManager.Instance.UpdateArea();
    }

    public void UpdateFillingStorage(float Amount)
    {
        incrementalSwingTime-=Amount;
        radialSwingTime=incrementalSwingTime;
        PlayerPrefs.SetFloat("incrementals",incrementalSwingTime);
        UIManager.Instance.UpdateSwingTime(incrementalSwingTime);
        //Playerprefs cekemedim
        interactUpgrade.CheckButtonsInteraction();
    }


    #endregion
    
}
