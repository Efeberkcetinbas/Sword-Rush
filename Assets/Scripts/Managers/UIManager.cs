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

    public Image ProgressBar;

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
