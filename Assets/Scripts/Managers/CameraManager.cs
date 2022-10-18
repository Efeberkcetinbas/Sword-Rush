using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public Camera mainCamera;

    [SerializeField] CinemachineVirtualCamera cm;
    //[SerializeField] CinemachineVirtualCamera cm2;


    public Transform cmCamera;
    //public Transform cm2Camera;


    Vector3 cameraInitialPosition;

    [Header("Shake Control")]
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.5f;

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
        /*cm2Camera.DOMove(new Vector3(xPos, yPos, zPos), duration).OnComplete(()=> {
           // DOTween.To(() => cm2.m_Priority, x => cm2.m_Priority = x, priority, duration);
        });
        cm2Camera.DORotate(new Vector3(xRot, yRot, 0), duration);*/
        /*DOTween.To(() => cm2.m_Lens.FieldOfView, x => cm2.m_Lens.FieldOfView = x, fieldOfView, duration);
        DOTween.To(() => cm2.m_Priority, x => cm2.m_Priority = x, priority, duration);*/
    }


    #region CameraShaker

    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePosition = mainCamera.transform.position;
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
    #endregion    


}
