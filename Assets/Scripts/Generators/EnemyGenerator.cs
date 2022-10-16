using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public void Init(string data,GameObject enemy){
        string[] problocs = data.Split((char)('|'));
        
        foreach (var item in problocs)
        {
            Instantiate(enemy,VectorHelper.StringToVector3(item),Quaternion.identity,transform).transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

}
