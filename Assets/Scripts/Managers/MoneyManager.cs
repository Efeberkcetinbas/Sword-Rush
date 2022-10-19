using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int moneyAmount;

   void Awake()
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

    void Start()
    {
        moneyAmount=PlayerPrefs.GetInt("money",1000);
        PlayerPrefs.SetInt("money",moneyAmount);
        UIManager.Instance.UpgradeMoneyText();
        //UIManager.Instance.UpgradeMoneyText();
    }

    public void UpdateMoney()
    {
        
        moneyAmount+=GameManager.Instance.EarningMoney;
        PlayerPrefs.SetInt("money",moneyAmount);
        UIManager.Instance.UpgradeMoneyText();
    }


}
