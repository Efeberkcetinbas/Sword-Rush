using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;


public class EffectToBar : MonoBehaviour
{
    public float speed;

    public Transform target;
    public GameObject coinPrefab;

    public Camera cam;

    void Start()
    {
        cam=Camera.main;
        target=UIManager.Instance.target;
    }

    public void StartCoinMove(Vector3 initial)
    {
        Vector3 targetPos=cam.ScreenToWorldPoint(new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z*-1));
        GameObject coin=Instantiate(coinPrefab,target);
        //coin.DOFade(0,1);
        Destroy(coin,3);
        Debug.Log("GOLD URETILDI");
        StartCoroutine(MoveCoin(coin.transform,initial,target));
    }

    IEnumerator MoveCoin(Transform obj,Vector3 startPos,Transform endPos)
    {
        Vector3 endPoint=cam.ScreenToWorldPoint(new Vector3(endPos.position.x,endPos.position.y,cam.transform.position.z*-1));
        obj.position=startPos;

        while((endPoint-obj.position).magnitude>0.5f)
        {
            obj.Translate((endPoint-obj.position).normalized*speed*Time.deltaTime);
            yield return new WaitForEndOfFrame();

            endPoint=cam.ScreenToWorldPoint(new Vector3(endPos.position.x,endPos.position.y,cam.transform.position.z*-1));
        }

        
    }
}
