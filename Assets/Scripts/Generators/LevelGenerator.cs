using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject propPrefab;

    [SerializeField] private GameObject bombPrefab;

    [Header("Controllers")]
    [SerializeField] PropGenerator propGenerator;
    //[SerializeField] WallGenerator wallGenerator;
    [SerializeField] EnemyGenerator enemyGenerator;

    [Header("Generation Strings")]
    [SerializeField] private string propGenerateString;


    //Konumlari buraya yazalim.
    [SerializeField] private string enemyGenerateString;

    [SerializeField] private string bombGenerateString;
    //[SerializeField] string wallGenerateString;

    /*[Header("GameOptions")]
    [SerializeField] Vector3 gameEndLocation;*/
    void Start()
    {

        if(!string.IsNullOrEmpty(propGenerateString)){
            propGenerator.Init(propGenerateString,propPrefab);
        }
        /*if(!string.IsNullOrEmpty(wallGenerateString)){
            wallGenerator.Init(wallGenerateString,inGameWallPrefab);
        }*/
        if(!string.IsNullOrEmpty(enemyGenerateString)){
            enemyGenerator.Init(enemyGenerateString,enemyPrefab);
        }

        //Baska scripte gerek kalmadan buradan halledebiliriz.
        if(!string.IsNullOrEmpty(bombGenerateString)){
            enemyGenerator.Init(bombGenerateString,bombPrefab);
        }
        //Instantiate(gameEndPrefab,gameEndLocation,Quaternion.identity,transform).transform.eulerAngles = new Vector3(0,-90,0);
    }
}
