using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBomb : MonoBehaviour
{
    [SerializeField] Explotion explotion;


    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Sword") && GameManager.Instance.canSwing)
        {
            CameraManager.Instance.ShakeIt();
            explotion.fireEffect.Play();
                    //Efekt olustur
            for (int i = 0; i < explotion.enemies.Count; i++)
            {
                explotion.enemies[i].GetComponent<EnemyTrigger>().Dead();
                
            }

            gameObject.SetActive(false);
            Destroy(explotion.gameObject,0.8f);
        }
    }
}
