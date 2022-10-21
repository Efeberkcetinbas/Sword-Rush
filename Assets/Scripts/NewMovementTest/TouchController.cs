using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private Vector2 _touchPosition;
    public Vector2 Direction;
    public Vector2 Rotation;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _touchPosition = eventData.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.position - _touchPosition;
        Direction = delta.normalized;
        Rotation = delta.normalized;
        //Bunu deneyebilirsin. Daha iyi oldu gibi.
        GameManager.Instance.Player.GetComponent<PlayerMovement>().playerAnimator.SetBool("attack",false);
        GameManager.Instance.sword.GetComponent<BoxCollider>().enabled=false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        //Debug.Log("RELEASE");
        //GameManager.Instance.swinging=true;
        if(GameManager.Instance.swinging && !GameManager.Instance.isPlayerDead)
        {
            GameManager.Instance.canSwing=true;
            GameManager.Instance.Player.GetComponent<PlayerMovement>().playerAnimator.SetBool("attack",true);
            StartCoroutine(ActiveCollider());
            //Bunu deneyebilirsin. Daha iyi oldu gibi.
            //GameManager.Instance.sword.GetComponent<BoxCollider>().enabled=true;
            GameManager.Instance.SwordPlayParticle();
        }

    }

    //Sword tam inerken bu efekti vermek icin kullaniyorum. Hanigis daha iyi sekilde duruyorsa ona karar ver. Belki gecikmeli olmasi iyi veya kotu etkiler.
    IEnumerator ActiveCollider()
    {
        yield return new WaitForSeconds(.4f);
        GameManager.Instance.sword.GetComponent<BoxCollider>().enabled=true;
    }
}
