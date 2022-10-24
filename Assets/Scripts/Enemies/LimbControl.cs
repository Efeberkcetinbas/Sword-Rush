using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbControl : MonoBehaviour
{
    [SerializeField] LimbControl[] childLimbs;

    [SerializeField] private GameObject limbPrefab;


   

    public void GetHit()
    {
        //Debug.Log("KAC KEZ CALISTI");
        if(childLimbs.Length>0)
        {
            foreach(LimbControl limb in childLimbs)
            {
                if(limb!=null)
                {
                    limb.GetHit();
                }
            }
        }


        if(limbPrefab!=null)
        {
            GameObject limbClone=(GameObject)Instantiate(limbPrefab, transform.position,transform.rotation);
            Destroy(limbClone,1);
            //Instantiate(limbPrefab, transform.position,transform.rotation);
        }

        transform.localScale=Vector3.zero;

        //Destroy(this);
        this.gameObject.SetActive(false);
    }

}
