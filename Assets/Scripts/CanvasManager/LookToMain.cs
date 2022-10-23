using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToMain : MonoBehaviour
{
     


    void Update()
    {
        LookAtControl();
    }

    void LookAtControl()
    {
        transform.LookAt(new Vector3(Camera.main.transform.position.x ,0 , 0));
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

   

}
