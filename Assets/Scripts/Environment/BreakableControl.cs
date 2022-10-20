using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableControl : MonoBehaviour
{
    [SerializeField] private GameObject scatteredObject;
    [SerializeField] private GameObject coinObject;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Sword") && GameManager.Instance.canDoDamage)
        {
            Create(scatteredObject);
            Destroy(gameObject);
            CameraManager.Instance.ShakeIt();
        }
    }


    private void Create(GameObject newgameObject)
    {
        GameObject clone=(GameObject)Instantiate(newgameObject,transform.position,Quaternion.identity);
        Destroy(clone,3);
    }
}
