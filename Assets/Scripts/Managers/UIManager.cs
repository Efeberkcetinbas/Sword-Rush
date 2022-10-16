using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI moneyText;

    public Image ProgressBar;

    public float lerpTime;

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

    public void ColorChanger()
    {
        //Matematiksel bir formul ile yaklas buraya.
        lerpTime+=0.1f;
        Color healthColor=Color.Lerp(Color.yellow,Color.red,(lerpTime));
        ProgressBar.color=healthColor;
    }

    /*public void UpgradeFromToLevelText()
    {
        FromLevelText.text = (LevelManager.Instance.levelIndex + 1).ToString();
        ToLevelText.text = (LevelManager.Instance.levelIndex + 2).ToString();
    }*/

    public void SetProgressbar(float val)
    {
        //Her mergeledigimizde arttiricagiz.
        ProgressBar.DOFillAmount(val, .25f);
    }
}
