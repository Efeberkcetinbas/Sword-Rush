using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Obstacable : MonoBehaviour
{
    float st = 0;
    internal float interval = 3;
    internal bool canInteract = true;
    internal string interactionTag = "Enemy";


    void OnTriggerEnter(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            StartInteractWithEnemy(other.GetComponent<EnemyTrigger>());
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            InteractWithEnemy(other.GetComponent<EnemyTrigger>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == interactionTag)
        {
            InteractionExit(other.GetComponent<EnemyTrigger>());
        }
    }

    void StartInteractWithEnemy(EnemyTrigger enemy)
    {
        DoAction(enemy);
    }

    void InteractWithEnemy(EnemyTrigger enemy)
    {
        st += Time.deltaTime;
        if (st > interval)
        {
            ResetProgress();
            DoAction(enemy);
        }
    }
    internal virtual void ResetProgress()
    {
        st = 0;
    }
    internal virtual void InteractionExit(EnemyTrigger enemy)
    {
        st = 0;
    }
    internal virtual void DoAction(EnemyTrigger enemy)
    {
        throw new System.NotImplementedException();
    }
    internal void StopInteract()
    {
        canInteract = false;
    }
    internal void StartInteract()
    {
        canInteract = true;
    }
}
