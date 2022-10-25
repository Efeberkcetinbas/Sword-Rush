using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Indexes")]
    public int levelIndex;
    public int backgroundIndex;
    public int newBackground;

    public List<GameObject> levels;

    public InteractUpgrade InteractUpgrade;
    
    public List<MeshRenderer> ground=new List<MeshRenderer>();
    public List<Material> groundMaterials=new List<Material>();



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
    private void LoadLevel()
    {
        levelIndex = PlayerPrefs.GetInt("LevelNumber");
        if (levelIndex == levels.Count) levelIndex = 1;
        PlayerPrefs.SetInt("LevelNumber", levelIndex);
        //UIManager.Instance.UpgradeLevelText();
        //ChangeGroundMaterial();
        //Daha güzel bir şekilde yaz bunları çok daginik.

        UIManager.Instance.UpgradeLevelText();
        UIManager.Instance.UpgradeMoneyText();
        UIManager.Instance.UpdateSwingTime(GameManager.Instance.incrementalSwingTime);
        UIManager.Instance.UpdateEarn();
        UIManager.Instance.UpdateArea();
        //Debug.Log(levels[levelIndex]);

        GameManager.Instance.DestroyGeneratedList();
        levels[levelIndex].GetComponent<LevelGenerator>().StartLevelGenerate();

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

        //GameManager.Instance.tapToPlayButton.SetActive(true);
        GameManager.Instance.isGameEnd=true;
        GameManager.Instance.isPlayerDead=false;
        GameManager.Instance.sword.transform.gameObject.SetActive(true);
        GameManager.Instance.Player.GetComponent<Animator>().SetFloat("speed",0);
        GameManager.Instance.Player.GetComponent<Animator>().SetBool("playerDead",false);
        GameManager.Instance.Player.GetComponent<Animator>().SetBool("success",false);
        GameManager.Instance.pointerArrow.SetActive(false);

        newBackground=PlayerPrefs.GetInt("RealLevel") + 1;
        backgroundIndex=PlayerPrefs.GetInt("backgroundIndex");
        StartCoroutine(ChangeColor(0));
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
        ChangeGroundMaterial();
    }

    IEnumerator CallCheckButtons()
    {
        yield return new WaitForSeconds(0.5f);
        InteractUpgrade.CheckButtonsInteraction();
    }

    private void ChangeGroundMaterial()
    {
        //Bunları Starta alip deneyebilirsin
        //backgroundIndex=PlayerPrefs.GetInt("backgroundIndex");


        if(newBackground % 9 == 0)
        {
            backgroundIndex++;
            PlayerPrefs.SetInt("backgroundIndex",backgroundIndex);


            if(backgroundIndex==groundMaterials.Count)
            {
                backgroundIndex=0;
                PlayerPrefs.SetInt("backgroundIndex",backgroundIndex);
            }

            StartCoroutine(ChangeColor(1));
        }
    }

    private IEnumerator ChangeColor(float time){

        yield return new WaitForSeconds(time);

        for (int i = 0; i < ground.Count; i++)
        {
            ground[i].material=groundMaterials[backgroundIndex];
        }
    }
}
