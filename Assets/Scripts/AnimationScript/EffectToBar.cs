using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EffectToBar : MonoBehaviour
{

    private Transform target;
    public GameObject coinPrefab;


    void Start()
    {
        target=UIManager.Instance.target;
    }

    public void StartCoinMove(Vector3 initial, GameObject a)
    {
        //Vector3 targetPos=cam.ScreenToWorldPoint(new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z*-1));
        GameObject coin=Instantiate(coinPrefab,a.transform.position,coinPrefab.transform.rotation,a.transform);
        coin.transform.parent=target;
        coin.transform.localScale=Vector3.one;
        coin.transform.DOLocalJump(coin.transform.localPosition,1,1,1,false);
        coin.transform.GetChild(0).transform.DOScale(Vector3.zero,2);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,3);
        //StartCoroutine(MoveCoin(coin.transform,initial,target));
    }
}
