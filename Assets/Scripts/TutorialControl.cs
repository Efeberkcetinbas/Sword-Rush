using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{

    public GameObject text1,text2;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text1.SetActive(false);
            text2.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text2.SetActive(false);
        }
    }
}
