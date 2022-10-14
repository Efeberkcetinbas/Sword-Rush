using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{//enemy turleri; stateleri olur.
    public NavMeshAgent agent;

    public LayerMask whatIsGround,whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //States
    public float sightRange;
    public bool playerInSightRange;

    void Awake()
    {
        agent=GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSightRange=Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);

        if(!playerInSightRange && GameManager.Instance.isPlayerHide) Patroling();
        if(playerInSightRange && !GameManager.Instance.isPlayerHide) ChasePlayer();
    }

    private void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint=transform.position-walkPoint;
        //Walkpoint reached
        if(distanceToWalkPoint.magnitude<1f)
            walkPointSet=false;
        

    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ=Random.Range(-walkPointRange,walkPointRange);
        float randomX=Random.Range(-walkPointRange,walkPointRange);

        walkPoint=new Vector3(transform.position.x+randomX,transform.position.y,transform.position.z+randomZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
            walkPointSet=true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(GameManager.Instance.Player.transform.position);
        LookingAt();
    }

    private void LookingAt()
    {
        //0.001 because of Look Rotation Viewing Vector Is Zero error.
        var lookPos = GameManager.Instance.Player.transform.position - transform.position;
        lookPos.y = 0+0.001f;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

   

    private void OnDrawGizmosSelected(){
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }


}
