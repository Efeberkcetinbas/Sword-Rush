using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableControl : MonoBehaviour
{
    [SerializeField] private GameObject scatteredObject;
    [SerializeField] private GameObject coinObject;
    private float randomNumber;


    void Start()
    {
        randomNumber=Random.Range(1,10);
    }
        

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Sword") && GameManager.Instance.canDoDamage)
        {
            //Debug.Log(randomNumber);
            Create(scatteredObject,3,transform.position);

            if(randomNumber<7)
                Create(coinObject,5,new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z));

            Destroy(gameObject);

            CameraManager.Instance.ShakeIt();
        }
    }


    private void Create(GameObject newgameObject,float time, Vector3 pos)
    {
        GameObject clone=(GameObject)Instantiate(newgameObject,pos,Quaternion.identity);
        Destroy(clone,time);
    }


    
}
