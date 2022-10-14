using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public FloatingJoystick floatingJoystick;
    Rigidbody playerRigidBody;
    internal Animator playerAnimator;

    internal bool canAttack=false;


    private float newSpeed;
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        //speed =GameManager.Instance.playerSpeed;
    }

    void Update()
    {
        DoSwordSwing();
    }

    public void FixedUpdate()
    {
        if (floatingJoystick.Vertical != 0 || floatingJoystick.Horizontal != 0)
        {
            Vector3 direction = Vector3.forward * floatingJoystick.Vertical * speed + Vector3.right * floatingJoystick.Horizontal * speed;

            transform.position += direction * speed * Time.deltaTime;
            playerRigidBody.velocity = direction;

            if (speed != 0)
            {
                if(playerRigidBody.velocity != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(playerRigidBody.velocity);
            }
            canAttack=false;
        }
        else if (floatingJoystick.Vertical == 0 && floatingJoystick.Horizontal == 0)
        {
            playerRigidBody.velocity = new Vector3(0, 0, 0);
            canAttack=true;
        }
        newSpeed = playerRigidBody.velocity.sqrMagnitude;
        playerAnimator.SetFloat("speed", Mathf.Abs(newSpeed));

    }

    public void DoSwordSwing()
    {
        if(Input.touchCount>0)
        {
            Touch touch=Input.GetTouch(0);

            if( newSpeed==0 && touch.phase==TouchPhase.Ended)
            {
                playerAnimator.SetBool("attack",true);
                GameManager.Instance.canSwing=true;
            }

            else
            {
                playerAnimator.SetBool("attack",false);
                GameManager.Instance.canSwing=false;
            }
        }
    }

}
