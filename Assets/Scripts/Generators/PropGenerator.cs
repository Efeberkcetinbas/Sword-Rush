using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    public void Init(string data,GameObject coin){
        string[] problocs = data.Split((char)('|'));
        
        foreach (var item in problocs)
        {
            Instantiate(coin,VectorHelper.StringToVector3(item),Quaternion.identity,transform).transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }
}
