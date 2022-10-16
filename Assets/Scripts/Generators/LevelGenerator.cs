using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    GameObject inGameWallPrefab;
    GameObject gameEndPrefab;

    [SerializeField] private GameObject enemyPrefab;

    [Header("Controllers")]
    /*[SerializeField] PropGenerator propGenerator;
    [SerializeField] WallGenerator wallGenerator;*/
    [SerializeField] EnemyGenerator enemyGenerator;

    [Header("Generation Strings")]
    //[SerializeField] string propGenerateString;


    //Konumlari buraya yazalim.
    [SerializeField] string enemyGenerateString;
    //[SerializeField] string wallGenerateString;

    /*[Header("GameOptions")]
    [SerializeField] Vector3 gameEndLocation;*/
    void Start()
    {

        /*if(!string.IsNullOrEmpty(propGenerateString)){
            propGenerator.Init(propGenerateString);
        }
        if(!string.IsNullOrEmpty(wallGenerateString)){
            wallGenerator.Init(wallGenerateString,inGameWallPrefab);
        }*/
        if(!string.IsNullOrEmpty(enemyGenerateString)){
            enemyGenerator.Init(enemyGenerateString,enemyPrefab);
        }
        //Instantiate(gameEndPrefab,gameEndLocation,Quaternion.identity,transform).transform.eulerAngles = new Vector3(0,-90,0);
    }
}
