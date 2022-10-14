using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    //Door Open, Close
    public event Action<int> onDoorwayTriggerEnter;
    public event Action<int> onDoorwayTriggerExit;
    public event Action<int> autoDoorTriggerEnter;
    public event Action<int> autoDoorTriggerExit;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #region Doors Region
    public void DoorwayTriggerEnter(int id)
    {
        if(onDoorwayTriggerEnter!=null)
        {
            onDoorwayTriggerEnter(id);
        }
    }

    public void DoorwayTriggerExit(int id)
    {
        if(onDoorwayTriggerExit!=null)
        {
            onDoorwayTriggerExit(id);
        }
    }
    
    #endregion

    public void AutoDoorTriggerEnter(int id)
    {
        if(autoDoorTriggerEnter!=null)
        {
            autoDoorTriggerEnter(id);
        }
    }

    public void AutoDoorTriggerExit(int id)
    {
        if(autoDoorTriggerExit!=null)
        {
            autoDoorTriggerExit(id);
        }
    }
}
