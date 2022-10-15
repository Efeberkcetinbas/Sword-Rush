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
        Debug.Log("sdasdasd");
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("DRAGGGG");
        var delta = eventData.position - _touchPosition;
        Direction = delta.normalized;
        Rotation = delta.normalized;
        GameManager.Instance.Player.GetComponent<PlayerMovement>().playerAnimator.SetBool("attack",false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        Debug.Log("RELEASE");
        //GameManager.Instance.swinging=true;
        if(GameManager.Instance.swinging)
        {
            GameManager.Instance.canSwing=true;
            GameManager.Instance.Player.GetComponent<PlayerMovement>().playerAnimator.SetBool("attack",true);
        }

    }
}
