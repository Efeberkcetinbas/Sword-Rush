using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{

    /*public float radius,force;

    public float sightRange;*/

    //public bool isEnemyInRange=false;

    //public LayerMask whatIsEnemy;
    public ParticleSystem fireEffect;

    public List<Transform>  enemies;
    private void OnTriggerEnter(Collider collider)
    {

        if(collider.CompareTag("Enemy"))
        {
            enemies.Add(collider.gameObject.transform);
        }

        /*if(Explode)
        {
            CameraManager.Instance.ShakeIt();
            fireEffect.Play();
                    //Efekt olustur
            Destroy(gameObject,0.6f);

            if(isEnemyInRange)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].GetComponent<EnemyTrigger>().Dead();
                }
            }*/
            

            /*Vector3 explotionPos=transform.position;
            Collider[] colliders=Physics.OverlapSphere(explotionPos,radius);

            foreach(Collider collider1 in colliders)
            {
                Rigidbody rb=collider.GetComponent<Rigidbody>();

                if(rb!=null)
                {
                    //0.1 Upwards modifier yani yukari hareketine bakiyor.
                    rb.AddExplosionForce(force,explotionPos,radius,15f,ForceMode.Force);
                    CameraManager.Instance.ShakeIt();
                    fireEffect.Play();
                    SoundManager.Instance.Play("grenade");
                    //Efekt olustur
                    Destroy(gameObject,1);
                }
            }*/
        //}
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Enemy"))
        {

            enemies.Remove(collider.gameObject.transform);
        
        }

    }
}
