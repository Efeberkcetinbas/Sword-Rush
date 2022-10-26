using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI moneyText;

    



    public Image ProgressBar;

    //RadialProgress Bar
    //public Image SwordFillBar;

    //Color change to Green when sword Fill Full
    //public Image FullBar;

    public Image playerHealthImage;
    public RectTransform fader;

    //public float lerpTime;

    [Header("Incrementals")]
    public TextMeshProUGUI incrementalSwingTime;
    public TextMeshProUGUI earnText;
    public TextMeshProUGUI swordAreaText;

    public Transform target;

    

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


    public void UpgradeLevelText()
    {
        LevelText.text = "Level " + (PlayerPrefs.GetInt("RealLevel") + 1).ToString();
    }

    public void UpgradeMoneyText()
    {
        moneyText.text = PlayerPrefs.GetInt("money").ToString();
    }

    public void ColorChanger(Image image,Color color1,Color color2,float thislerpTime)
    {
        //Matematiksel bir formul ile yaklas buraya.
        float lerpTime=0;
        lerpTime+=thislerpTime;
        Color healthColor=Color.Lerp(color1,color2,(lerpTime));
        image.color=healthColor;
    }

    public void UpdateSwingTime(float time)
    {
        time=(float)Math.Round(time,2);
        incrementalSwingTime.text=time.ToString();
    }

    public void UpdateEarn()
    {
        //10 hane gozukuyor bazen onu duzelt.
        earnText.text=GameManager.Instance.EarningMoney.ToString();
    }

    public void UpdateArea()
    {
        swordAreaText.text=GameManager.Instance.SwordArea.ToString();
    }

    public void UpdateHealthBar(float val)
    {
        playerHealthImage.DOFillAmount(val,0.05f);
    }




    /*public void UpgradeFromToLevelText()
    {
        FromLevelText.text = (LevelManager.Instance.levelIndex + 1).ToString();
        ToLevelText.text = (LevelManager.Instance.levelIndex + 2).ToString();
    }*/

    /*public void SetRadialProgressBar(float val)
    {
        SwordFillBar.fillAmount=val;
    }*/

    public void SetProgressbar(float val)
    {
        //Her mergeledigimizde arttiricagiz.
        ProgressBar.DOFillAmount(val, .25f);
    }

    public void StartFader()
    {
        fader.gameObject.SetActive(true);

        fader.DOScale(new Vector3(3,3,3),1).OnComplete(()=>{
            fader.DOScale(Vector3.zero,1f).OnComplete(()=>fader.gameObject.SetActive(false)).OnComplete(()=>GameManager.Instance.OpenIncrementalPanel());
        });
    }
}
