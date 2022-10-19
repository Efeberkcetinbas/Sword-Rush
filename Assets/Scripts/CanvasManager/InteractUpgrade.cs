using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InteractUpgrade : MonoBehaviour
{
    [Header("Prices")]
    public float timeDecreasePrice;
    public float areaPrice;
    public float earnPrice;


    [Header("Buttons")]

    public Button timeDecreaseButton;
    public Button areaButton;
    public Button earnButton;

    [Header("Texts")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI areaText;
    public TextMeshProUGUI earnText;

    private Color areaColor;
    private Color timeColor;
    private Color earnColor;    

    MoneyManager moneyManager;
    void Start()
    {
        moneyManager=MoneyManager.Instance;
        earnPrice=PlayerPrefs.GetFloat("earn",100);
        timeDecreasePrice=PlayerPrefs.GetFloat("timeDecrease",50);
        areaPrice=PlayerPrefs.GetFloat("area",75);
        UpdateText(timeText,timeDecreasePrice);
        UpdateText(areaText,areaPrice);
        UpdateText(earnText,earnPrice);
    }

    

    public void BuyButtonDecrease()
    {
        if(moneyManager.moneyAmount>=timeDecreasePrice)
        {
            //PlayerPrefs.GetFloat("timeDecrease");
            NewMoneyAmount(timeDecreasePrice);
            ButtonActive(timeDecreaseButton,true);
            timeDecreasePrice=Mathf.RoundToInt(timeDecreasePrice*1.3f);
            PlayerPrefs.SetFloat("timeDecrease",timeDecreasePrice);
            UpdateText(timeText,timeDecreasePrice);
            MakeTween(timeDecreaseButton.gameObject);
        }

        else
        {
            ButtonActive(timeDecreaseButton,false);
        }
    }

    
    public void BuyButtonArea()
    {
        if(moneyManager.moneyAmount>=areaPrice)
        {
            //PlayerPrefs.GetFloat("area");
            NewMoneyAmount(areaPrice);
            ButtonActive(areaButton,true);
            areaPrice=Mathf.RoundToInt(areaPrice*1.5f);
            PlayerPrefs.SetFloat("area",areaPrice);
            UpdateText(areaText,areaPrice);
            MakeTween(areaButton.gameObject);

            //Check methodu yaz. Her para arttiginda bunlari kontrol etsin. Check methodunu her level degistiginde cagir
        }

        else
        {
            ButtonActive(areaButton,false);
        }
    }

    public void BuyButtonEarn()
    {
        if(moneyManager.moneyAmount>=earnPrice)
        {
            //PlayerPrefs.GetFloat("earn");
            NewMoneyAmount(earnPrice);
            ButtonActive(earnButton,true);
            earnPrice=Mathf.RoundToInt(earnPrice*1.2f);
            PlayerPrefs.SetFloat("earn",earnPrice);
            UpdateText(earnText,earnPrice);
            MakeTween(earnButton.gameObject);
        }

        else
        {
            ButtonActive(earnButton,false);
        }
    }

    public bool ButtonActive(Button interactableButton,bool tf)
    {
        return interactableButton.interactable=tf;
    }

    public void NewMoneyAmount(float price)
    {
        moneyManager.moneyAmount=Mathf.RoundToInt(moneyManager.moneyAmount-price);
        PlayerPrefs.SetInt("money",moneyManager.moneyAmount);
        UIManager.Instance.UpgradeMoneyText();
    }

    public void UpdateText(TextMeshProUGUI TextP, float price)
    {
        TextP.text=price.ToString();
    }

    private void ChangeColor(Button buttonName,Color color)
    {
        ColorBlock cb=buttonName.colors;
        cb.normalColor=color;
        buttonName.colors=cb;
    }

    private void MakeTween(GameObject gameObject)
    {
        gameObject.transform.DOScale(new Vector3(0.75f,0.75f,0.75f),0.25f).OnComplete(()=>gameObject.transform.DOScale(Vector3.one,0.25f));
    }

    public void CheckButtonsInteraction()
    {

        if(moneyManager.moneyAmount>=areaPrice)
        {
            ButtonActive(areaButton,true);
            ChangeColor(areaButton,Color.yellow);
        }

        if(moneyManager.moneyAmount<areaPrice)
        {
            ButtonActive(areaButton,false);
            ChangeColor(areaButton,Color.grey);

        }
        
        if(moneyManager.moneyAmount>=earnPrice)
        {
            ButtonActive(earnButton,true);
            ChangeColor(earnButton,Color.yellow);

        }
        
        if(moneyManager.moneyAmount<earnPrice)
        {
            ButtonActive(earnButton,false);
            ChangeColor(earnButton,Color.grey);

        }

        if(moneyManager.moneyAmount>=timeDecreasePrice)
        {
            ButtonActive(timeDecreaseButton,true);
            ChangeColor(timeDecreaseButton,Color.yellow);

        }

        if(moneyManager.moneyAmount<timeDecreasePrice)
        {
            ButtonActive(timeDecreaseButton,false);
            ChangeColor(timeDecreaseButton,Color.grey);

        }

    }

}
