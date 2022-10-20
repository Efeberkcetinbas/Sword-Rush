using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineControl : MonoBehaviour
{

    //Abstracttan cekilince defalarca girip cikiyor. Bug.
    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            GameManager.Instance.isGameEnd=true;
            GameManager.Instance.OpenSuccessLevel();
            CameraManager.Instance.ChangeCameras(45,0.5f,GameManager.Instance.Player);
        }
    }
}
