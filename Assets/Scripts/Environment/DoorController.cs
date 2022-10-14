using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public int id;

    [SerializeField] private float x1,x2,old_x1,old_x2,duration;

    [SerializeField] private GameObject door1,door2;
    void Start()
    {
        GameEvents.Instance.onDoorwayTriggerEnter+=OnDoorwayOpen;
        GameEvents.Instance.onDoorwayTriggerExit+=OnDoorwayClose;
    }

    private void OnDoorwayOpen(int id)
    {
        if(id==this.id)
        {
            door1.transform.DOLocalMoveX(x1,duration);
            door2.transform.DOLocalMoveX(x2,duration);
        }
    }

    private void OnDoorwayClose(int id)
    {
        if(id==this.id)
        {
            door1.transform.DOLocalMoveX(old_x1,duration);
            door2.transform.DOLocalMoveX(old_x2,duration);
        }
    }
    
    private void OnDestroy()
    {
        GameEvents.Instance.onDoorwayTriggerEnter-=OnDoorwayOpen;
        GameEvents.Instance.onDoorwayTriggerExit-=OnDoorwayClose;
    }
}
