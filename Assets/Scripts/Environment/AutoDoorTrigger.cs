using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoorTrigger : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameEvents.Instance.AutoDoorTriggerEnter(id);
            GameManager.Instance.isPlayerHide=!GameManager.Instance.isPlayerHide;
            //Debug.Log("dasdasdasdasd");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameEvents.Instance.AutoDoorTriggerExit(id);
        }
            
    }
}
