using System;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private bool haveInteracted = false;
    public void OnClick(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        // 来到物体附近
        playerAgent.stoppingDistance = 2.2f;
        playerAgent.SetDestination(transform.position);
        haveInteracted = false;
        // 进行交互
        // Interact();
    }

    private void Update()
    {
        if (playerAgent != null && playerAgent.pathPending == false)
        {
            if (playerAgent.remainingDistance <= 2.2f && !haveInteracted)
            {
                // 进行交互
                Interact();
                haveInteracted = true;
            }
        }
    }

    protected virtual void Interact()
    {
        print("Interacting with Interactable Item");
    }
}