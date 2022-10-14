using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoDoorControl : MonoBehaviour
{
     public int id;

    [SerializeField] private float y1,y2,old_y1,old_y2,duration;

    [SerializeField] private GameObject door1,door2;
    void Start()
    {
        GameEvents.Instance.autoDoorTriggerEnter+=AutoDoorOpen;
        GameEvents.Instance.autoDoorTriggerExit+=AutoDoorClose;
    }

    private void AutoDoorOpen(int id)
    {
        if(id==this.id)
        {
            door1.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, y1, 0)), duration);
            door2.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, y2, 0)), duration);
        }
    }

    private void AutoDoorClose(int id)
    {
        if(id==this.id)
        {
            door1.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, old_y1, 0)), duration);
            door2.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, old_y2, 0)), duration);
        }
    }
    
    private void OnDestroy()
    {
        GameEvents.Instance.onDoorwayTriggerEnter-=AutoDoorOpen;
        GameEvents.Instance.onDoorwayTriggerExit-=AutoDoorClose;
    }


}
