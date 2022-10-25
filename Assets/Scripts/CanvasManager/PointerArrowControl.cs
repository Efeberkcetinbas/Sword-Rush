using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrowControl : MonoBehaviour
{

    void Update()
    {
        transform.position=new Vector3(GameManager.Instance.Player.transform.position.x,GameManager.Instance.Player.transform.position.y+0.5f,GameManager.Instance.Player.transform.position.z);
        transform.LookAt(GameManager.Instance.FinishLinePos);
    }

}
