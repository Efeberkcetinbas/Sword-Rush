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
        transform.LookAt(transform.position+mLookAt.transform.rotation*Vector3.back,
        mLookAt.transform.rotation*Vector3.down);
    }

}
