using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableControl : MonoBehaviour
{
    [SerializeField] private GameObject scatteredObject;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Sword") && GameManager.Instance.canSwing)
        {
            Create(scatteredObject);
            Destroy(gameObject);
            CameraManager.Instance.ShakeIt();
        }
    }


    private GameObject Create(GameObject gameObject)
    {
        return Instantiate(gameObject,transform.position,Quaternion.identity);
    }
}
