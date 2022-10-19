using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBomb : MonoBehaviour
{
    [SerializeField] Explotion explotion;


    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Sword") && GameManager.Instance.canDoDamage)
        {
            CameraManager.Instance.ShakeIt();
            explotion.fireEffect.Play();
            SoundManager.Instance.Play("grenade");
            
            for (int i = 0; i < explotion.enemies.Count; i++)
            {
                if(explotion.enemies[i]!=null)
                    explotion.enemies[i].GetComponent<EnemyTrigger>().Dead();
            }

            gameObject.SetActive(false);
            Destroy(explotion.gameObject,0.8f);
        }
    }
}
