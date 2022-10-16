using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Obstacable : MonoBehaviour
{
    float st = 0;
    internal float interval = 3;
    internal bool canInteract = true;
    internal string interactionTag = "Player";


    void OnTriggerEnter(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            StartInteractWithEnemy(other.GetComponent<PlayerTrigger>());
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            InteractWithEnemy(other.GetComponent<PlayerTrigger>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == interactionTag)
        {
            InteractionExit(other.GetComponent<PlayerTrigger>());
        }
    }

    void StartInteractWithEnemy(PlayerTrigger player)
    {
        DoAction(player);
    }

    void InteractWithEnemy(PlayerTrigger player)
    {
        st += Time.deltaTime;
        if (st > interval)
        {
            ResetProgress();
            DoAction(player);
        }
    }
    internal virtual void ResetProgress()
    {
        st = 0;
    }
    internal virtual void InteractionExit(PlayerTrigger player)
    {
        st = 0;
    }
    internal virtual void DoAction(PlayerTrigger player)
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
