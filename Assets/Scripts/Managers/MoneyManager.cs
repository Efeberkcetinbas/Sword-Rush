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
        moneyAmount=PlayerPrefs.GetInt("money",moneyAmount);
    }

    public void UpdateMoney(int increaseMoney)
    {
        moneyAmount+=increaseMoney;
        PlayerPrefs.SetInt("money",moneyAmount);
        UIManager.Instance.UpgradeMoneyText();

    }
}
