using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] CinemachineVirtualCamera cm;
    [SerializeField] CinemachineVirtualCamera cm2;


    public Transform cmCamera;
    public Transform cm2Camera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    
    public void ChangeFieldOfView(float fieldOfView, float duration = 1)
    {
        //DOTween.To(() => cm.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, x => cm.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = x, offset, duration);
        //DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration);
    }

    public void ChangeDirection(float rotateX, float positionY, float duration, float specialFieldOfView)
    {
        //cm.m_LookAt = target;
        cmCamera.DOMoveY(positionY, duration).OnComplete(() =>
        {
        });
        cmCamera.DOLocalRotate(new Vector3(rotateX, 0, 0), duration * 1.25f);
        //DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, specialFieldOfView, duration);
    }

    //En sondaki bullet takibi
    public void FollowTheBullet(float fieldOfView,float duration,Transform target)
    {
        /*cm2.m_Priority = 11;
        cm2.m_LookAt = target;
        cm2.m_Follow = target;

        DOTween.To(() => cm2.m_Lens.FieldOfView, x => cm2.m_Lens.FieldOfView = x, fieldOfView, duration);*/
    }

    public void ResetCamera()
    {
        //cm2.m_Priority = 9;
    }

    public void ChangeCameras(float fieldOfView, float duration, float xPos, float yPos, float zPos, float xRot, float yRot)
    {
        cmCamera.DOLocalMove(new Vector3(xPos, yPos, zPos), duration);
        cmCamera.DOLocalRotate(new Vector3(xRot, yRot, 0), duration);
        //DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration);
    }

    public void ChangeCameraAngle(float fieldOfView, float duration, float xPos, float yPos, float zPos, float xRot, float yRot, int priority)
    {
        cm2Camera.DOMove(new Vector3(xPos, yPos, zPos), duration).OnComplete(()=> {
           // DOTween.To(() => cm2.m_Priority, x => cm2.m_Priority = x, priority, duration);
        });
        cm2Camera.DORotate(new Vector3(xRot, yRot, 0), duration);
        /*DOTween.To(() => cm2.m_Lens.FieldOfView, x => cm2.m_Lens.FieldOfView = x, fieldOfView, duration);
        DOTween.To(() => cm2.m_Priority, x => cm2.m_Priority = x, priority, duration);*/
    }
}
