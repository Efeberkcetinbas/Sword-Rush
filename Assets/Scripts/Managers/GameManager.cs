using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public int EnemyCounter;
    public float increaseValue;
    public float ProgressValue;
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
   
    public void UpdateEnemyCounter()
    {
        EnemyCounter=FindObjectOfType<CountOfEnemy>().counter;
        increaseValue=1/(float)GameManager.Instance.EnemyCounter;
    }

    public void ResetTheLevel()
    {
        ProgressValue=0;
        UIManager.Instance.ProgressBar.DOFillAmount(0, .1f);
    }
}
