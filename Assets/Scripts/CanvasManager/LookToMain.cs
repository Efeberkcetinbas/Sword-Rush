using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToMain : MonoBehaviour
{
    [SerializeField] private Transform mLookAt;

    private Transform localTrans;
    

    void Start()
    {
        localTrans=GetComponent<Transform>();
    }

    void Update()
    {
        if(mLookAt)
            localTrans.LookAt(2*localTrans.position-mLookAt.position);
    }

}
