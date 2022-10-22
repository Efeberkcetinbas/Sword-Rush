using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{

    public ParticleSystem fireEffect;

    public List<Transform>  enemies;
    private void OnTriggerEnter(Collider collider)
    {

        if(collider.CompareTag("Enemy"))
        {
            enemies.Add(collider.gameObject.transform);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            enemies.Remove(collider.gameObject.transform);
        }

    }
}
