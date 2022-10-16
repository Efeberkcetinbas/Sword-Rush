using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    [Header("Bools")]
    public bool isPlayerHide=false;
    public bool swinging=false;
    public bool canSwing=true;
    public bool stopTime=false;

    [Header("Times and Values")]
    public float swingTime;
    public float incrementalSwingTime;

    public int EnemyCounter;
    public float increaseValue;
    public float ProgressValue;

    [Header("Particles")]
    public ParticleSystem splashParticle;
    public ParticleSystem barfullParticle;


    public Vector3 PlayerInitPos;

    public Transform sword;

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

    }

    private void Update()
    {
        //swinging=False
        if(canSwing)
        {
            swinging=false;
            swingTime+=Time.deltaTime;
            if(swingTime>incrementalSwingTime)
            {
                swinging=true;
                swingTime=0;
                canSwing=false;
            }
        }
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
        //Incrementaller scripti eklendiginde matematiksel ifadeyle yaz.
        //Yani incrementaldeki degeri al carpimi seklinde yaz.
        //PlayerPref ile tutarsin
        var val=sword.transform.localScale.x;
        Debug.Log("VAL : " + val);
        stopTime=true;
        sword.transform.DOScale(new Vector3(val*10,val*10,val*10),1f).OnComplete(()=>sword.transform.DOScale(new Vector3(val,val,val),1f));
    }

    
}
