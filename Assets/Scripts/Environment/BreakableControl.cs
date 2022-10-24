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
            Debug.Log(randomNumber);
            Create(scatteredObject,3);

            if(randomNumber%2==0)
                Create(coinObject,5);

            Destroy(gameObject);

            CameraManager.Instance.ShakeIt();
        }
    }


    private void Create(GameObject newgameObject,float time)
    {
        GameObject clone=(GameObject)Instantiate(newgameObject,transform.position,Quaternion.identity);
        Destroy(clone,time);
    }

    
}
