using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Indexes")]
    public int levelIndex;
    public List<GameObject> levels;

    public InteractUpgrade InteractUpgrade;

    //[SerializeField] RectTransform fader;

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
        LoadLevel();
    }
    void LoadLevel()
    {
        levelIndex = PlayerPrefs.GetInt("LevelNumber");
        if (levelIndex == levels.Count) levelIndex = 0;
        PlayerPrefs.SetInt("LevelNumber", levelIndex);
        //UIManager.Instance.UpgradeLevelText();

        //Daha güzel bir şekilde yaz bunları çok daginik.
        UIManager.Instance.UpgradeLevelText();
        UIManager.Instance.UpgradeMoneyText();
        UIManager.Instance.UpdateSwingTime(GameManager.Instance.incrementalSwingTime);
        UIManager.Instance.UpdateEarn();
        UIManager.Instance.UpdateArea();

        GameManager.Instance.ResetGameEnds();

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
        levels[levelIndex].SetActive(true);
        GameManager.Instance.UpdateEnemyCounter();
        GameManager.Instance.UpdateFinishPos();
        //GameManager.Instance.OpenIncrementalPanel();
        StartCoroutine(CallCheckButtons());

        GameManager.Instance.tapToPlayButton.SetActive(true);
        GameManager.Instance.isGameEnd=true;
        GameManager.Instance.isPlayerDead=false;
        GameManager.Instance.sword.transform.gameObject.SetActive(true);
        GameManager.Instance.Player.GetComponent<Animator>().SetBool("playerDead",false);
        GameManager.Instance.Player.GetComponent<Animator>().SetBool("success",false);

        //DotweenManager.Instance.FadeTween(fader, 0, 0, 0, .25f);
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("LevelNumber", levelIndex + 1);
        PlayerPrefs.SetInt("RealLevel", PlayerPrefs.GetInt("RealLevel", 0) + 1);
        LoadLevel();
        GameManager.Instance.ResetTheLevel();
        //Startda calismasini istiyorsan loadLevel methodu icine yazacaksin. Diger turlu burada duracak.
        UIManager.Instance.StartFader();
        //GameManager.Instance.GetAllPlayerPrefs();
    }

    IEnumerator CallCheckButtons()
    {
        yield return new WaitForSeconds(0.5f);
        InteractUpgrade.CheckButtonsInteraction();
    }
}
