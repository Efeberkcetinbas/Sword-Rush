using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;
    public GameObject IncrementalPanel;
    public GameObject successPanel;
    public GameObject failPanel;
    public GameObject gameEndCanvas;
    public GameObject finishLineObject;
    public GameObject tapToPlayButton;
    public GameObject pointerArrow;


    [Header("Bools")]
    public bool isPlayerHide=false;
    public bool swinging=false;
    //canSwing kesme hareketine yariyor.
    public bool canSwing=true;
    public bool canDoDamage=false;
    public bool isPlayerDead=false;
    public bool isGameEnd=false;
    public bool canProgressContinue=true;
    public bool isAnExplotion=false;

    private bool isTapOpen=false;

    [Header("Times and Values")]
    public float swingTime;
    public float incrementalSwingTime;
    public float radialSwingTime;
    public float damageTime;

    public int EnemyCounter;
    public int SumOfEnemies;
    public int EarningMoney;
    public float increaseValue;
    public float ProgressValue;
    public float SwordArea;

    [Range(0,1.5f)]public float SelectDamageTime;

    [Header("Particles")]
    public ParticleSystem splashParticle;
    public ParticleSystem barfullParticle;

    public List<ParticleSystem> gameEndParticles=new List<ParticleSystem>();
    public List<GameObject> generatedObjects=new List<GameObject>();



    public Vector3 PlayerInitPos;
    public Vector3 PlayerInitRot;
    public Vector3 FinishLinePos;

    public Transform sword;

    public InteractUpgrade interactUpgrade;

    [SerializeField]private LevelGenerator levelGenerator;


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
        sword.transform.localScale=new Vector3(1.5f+SwordArea,1,1.5f+SwordArea);
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
            //UIManager.Instance.FullBar.color=new Color(1f,1f,1f,0.2392f);
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
                //UIManager.Instance.FullBar.color=new Color(0,1f,0.0071f,0.2392f);
                //Hissiyat acisindan bu daha iyi oldu.
            }
        }

        if(Input.touchCount>0)
        {
            if(isTapOpen)
            {
                TapToPlay();
                //Debug.Log("FENERBAHCE : 1");
            }

        }        

        //UIManager.Instance.SetRadialProgressBar(radialSwingTime/incrementalSwingTime);
    }
    

   
   
    public void UpdateEnemyCounter()
    {
        EnemyCounter=FindObjectOfType<CountOfEnemy>().ultiCounter;
        SumOfEnemies=FindObjectOfType<CountOfEnemy>().whatIsEnemyCount;
        increaseValue=1/(float)EnemyCounter;
    }

    public void UpdateFinishPos()
    {
        FinishLinePos=FindObjectOfType<FinishPosition>().finishPosition;
        finishLineObject.transform.position=FinishLinePos;
    }

    public void ResetTheLevel()
    {
        ProgressValue=0;
        UIManager.Instance.ProgressBar.DOFillAmount(0, .1f);
        Player.transform.position=PlayerInitPos;
        Player.transform.rotation=Quaternion.Euler(PlayerInitRot);
        Player.GetComponent<PlayerTrigger>().currentHealth=Player.GetComponent<PlayerTrigger>().maxHealth;
        UIManager.Instance.UpdateHealthBar((float)Player.GetComponent<PlayerTrigger>().currentHealth/(float)Player.GetComponent<PlayerTrigger>().maxHealth);
    }

    public void SwordPlayParticle()
    {
        splashParticle.Play();
    }

    public void BarFullPlayParticle()
    {
        barfullParticle.Play();
    }

    public void OpenSuccessLevel()
    {
        gameEndCanvas.SetActive(true);
        successPanel.SetActive(true);
        failPanel.SetActive(false);


        Player.GetComponent<Animator>().SetBool("success",true);

        for (int i = 0; i < gameEndParticles.Count; i++)
        {
            gameEndParticles[i].Play();
        }

    }

    public void RestartButton()
    {
        failPanel.SetActive(false);
        GameManager.Instance.Player.GetComponent<Animator>().SetFloat("speed",0);
        GameManager.Instance.Player.GetComponent<Animator>().SetBool("playerDead",false);
        GameManager.Instance.Player.GetComponent<Animator>().SetBool("success",false);
        GameManager.Instance.isGameEnd=true;
        GameManager.Instance.isPlayerDead=false;
        DestroyGeneratedList();
        ResetTheLevel();
        StartCoroutine(CallGenerate());
    }

    public void DestroyGeneratedList()
    {

        for (int i = 0; i < generatedObjects.Count; i++)
        {
            Destroy(generatedObjects[i]);
        }

        generatedObjects=new List<GameObject>();
        // Bundan daha hizli ve asagidaki daha cok memory kullaniyor
        //generatedObjects.Clear();

    }
    private IEnumerator CallGenerate()
    {
        yield return new WaitForSeconds(0.5f);
        levelGenerator=FindObjectOfType<LevelGenerator>();
        levelGenerator.StartLevelGenerate();
    }


    public void ActivatePointerArrow()
    {
        pointerArrow.SetActive(true);
    }

    



    public void OpenFailLevel()
    {
        gameEndCanvas.SetActive(true);
        successPanel.SetActive(false);
        failPanel.SetActive(true);
    }

    public void ResetGameEnds()
    {
        gameEndCanvas.SetActive(false);
        successPanel.SetActive(false);
        failPanel.SetActive(false);

        for (int i = 0; i < gameEndParticles.Count; i++)
        {
            gameEndParticles[i].Stop();
        }

        CameraManager.Instance.ResetCamera();
    }

    public void IncreaseSwordAreaCollider()
    {
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        var val=sword.transform.localScale.x;
        float oldSelectedTime=SelectDamageTime;
        SelectDamageTime=3;
        //Debug.Log("VAL : " + val);
        //Buyuk ihtimal buradan dolayi dotween hatasi aliyorum. Buraya bak !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        sword.transform.DOScale(new Vector3(val*2,1,val*2),0.5f).OnComplete(()=>{
            sword.transform.DOScale(new Vector3(val,1,val),0.5f).OnComplete(()=>{
                SelectDamageTime=oldSelectedTime;
            });
        });
        //sword.transform.DOScale(new Vector3(val*7,1,val*7),1f).OnComplete(()=>sword.transform.DOScale(new Vector3(val,1,val),1f));
    }

    public void TapToPlay()
    {
        isGameEnd=false;
        tapToPlayButton.SetActive(false);
        isTapOpen=false;
    }

    public void GetAllPlayerPrefs()
    {
        //0.4'e kadar oluyor
        incrementalSwingTime=PlayerPrefs.GetFloat("incrementals",1f);
        radialSwingTime=incrementalSwingTime;
        EarningMoney=PlayerPrefs.GetInt("earningMoney",10);
        SwordArea=PlayerPrefs.GetFloat("areaHit",0.1f);
    }

    #region Incremental

    public void CloseIncrementalPanel()
    {
        IncrementalPanel.transform.DOScale(Vector3.zero,0.25f).OnComplete(()=>
        {
            IncrementalPanel.SetActive(false);
            tapToPlayButton.SetActive(true);
            isTapOpen=true;
            
        });
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
        //Debug.Log("SWORD LOCAL SCALE :" + SwordArea);
        sword.transform.localScale=new Vector3(1.5f+SwordArea,1,1.5f+SwordArea);
        PlayerPrefs.SetFloat("areaHit",SwordArea);
        interactUpgrade.CheckButtonsInteraction();
        UIManager.Instance.UpdateArea();
    }

    public void UpdateFillingStorage(float Amount)
    {
        if(incrementalSwingTime>=0.4f)
        {
            incrementalSwingTime-=Amount;
            radialSwingTime=incrementalSwingTime;
            PlayerPrefs.SetFloat("incrementals",incrementalSwingTime);
            UIManager.Instance.UpdateSwingTime(incrementalSwingTime);
            interactUpgrade.CheckButtonsInteraction();
        }
        
    }


    #endregion
    
}
