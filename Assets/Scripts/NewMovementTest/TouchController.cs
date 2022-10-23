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
        //Duruma gore acip kapayabilirsin.
        GameManager.Instance.sword.GetComponent<SphereCollider>().enabled=false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        //Debug.Log("RELEASE");
        //GameManager.Instance.swinging=true;
        if(GameManager.Instance.swinging && !GameManager.Instance.isPlayerDead && !GameManager.Instance.isGameEnd)
        {
            GameManager.Instance.canSwing=true;
            GameManager.Instance.Player.GetComponent<PlayerMovement>().playerAnimator.SetBool("attack",true);
            //GameManager.Instance.sword.GetComponent<SphereCollider>().enabled=true;
            //Buradan sureyi oynayarak en iyi sureye karar ver. 0.5f
            StartCoroutine(Rotate(0.35f));
            StartCoroutine(ActiveCollider());
            //Bunu deneyebilirsin. Daha iyi oldu gibi.
            //GameManager.Instance.sword.GetComponent<BoxCollider>().enabled=true;
            GameManager.Instance.SwordPlayParticle();
        }

    }

    IEnumerator Rotate(float duration)
    {
        float startRotation = GameManager.Instance.Player.transform.eulerAngles.y;
        float endRotation = startRotation + 720.0f;
        float t = 0.0f;
        while ( t  < duration )
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 720.0f;
            GameManager.Instance.Player.transform.eulerAngles = new Vector3( GameManager.Instance.Player.transform.eulerAngles.x, yRotation,  GameManager.Instance.Player.transform.eulerAngles.z);
            yield return null;
        }
    }

    //Sword tam inerken bu efekti vermek icin kullaniyorum. Hanigis daha iyi sekilde duruyorsa ona karar ver. Belki gecikmeli olmasi iyi veya kotu etkiler.
    IEnumerator ActiveCollider()
    {
        yield return new WaitForSeconds(.1f);
        GameManager.Instance.sword.GetComponent<SphereCollider>().enabled=true;
    }

    
}
