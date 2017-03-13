using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    public NavMeshAgent agent;
    public float inputHoldDelay = 0.5f;
    public float turnSpeedThreshold = 0.5f;
    public float speedDampTime = 0.1f;
    public float slowingSpeed = 0.175f;
    public float turnSmoothing = 15f;

    private WaitForSeconds inputHoldWait;
    private Vector3 DestinationPosition;
    private Interactable currentInteractable;
    private bool handleInput = true;

    private const float stopDistanceProportion = 0.1f;
    private const float navMeshSampleDistance = 4f;

    private readonly int hashSpeedPara = Animator.StringToHash("Speed");
    private readonly int hashLocomotionTag = Animator.StringToHash("Locomotion");

    private void Start ()
    {
        agent.updateRotation = false;

        inputHoldWait = new WaitForSeconds(inputHoldDelay);

        DestinationPosition = transform.position;


    }

    private void OnAnimatorMove ()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;

    }

    private void Update ()
    {
        if (agent.pathPending)
        {
            return;
        }

        float speed = agent.desiredVelocity.magnitude;

        if (agent.remainingDistance <= agent.stoppingDistance *stopDistanceProportion)
        {
            Stopping(out speed);
        }

        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Slowing(out speed, agent.remainingDistance);
        }

        else if (speed > turnSpeedThreshold)
        {
            Moving();
        }

        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);

    }


    private void Stopping (out float speed)
    {
        agent.Stop();
        transform.position = DestinationPosition;
        speed = 0f;

        if (currentInteractable)
        {
            transform.rotation = currentInteractable.interactionLocation.rotation;
            currentInteractable.Interact();
            currentInteractable = null;

            StartCoroutine(WaitForInteraction());
        }


    }

    private void Slowing (out float speed, float distanceToDestination)
    {
        agent.Stop();
        transform.position = Vector3.MoveTowards(transform.position, DestinationPosition, slowingSpeed * Time.deltaTime);

        float proportionalDisctance = 1f - distanceToDestination / agent.stoppingDistance;
        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDisctance);

        Quaternion targetRotation = currentInteractable ? currentInteractable.interactionLocation.rotation : transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDisctance);
    }

    private void Moving ()
    {

        Quaternion targetRoatation = Quaternion.LookRotation(agent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRoatation, turnSmoothing * Time.deltaTime);
    }

    public void OnGroundClick (BaseEventData data)
    {
        if (!handleInput)
        {
            return;

        }

        currentInteractable = null;

        PointerEventData pData = (PointerEventData)data;
        NavMeshHit hit;

        if (NavMesh.SamplePosition (pData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            DestinationPosition = hit.position;
        }
        else
        {
            DestinationPosition = pData.pointerCurrentRaycast.worldPosition;
        }

        agent.SetDestination(DestinationPosition);

        agent.Resume();

    }

    public void OnInteractableClick (Interactable interactable)
    {
        if (!handleInput)
        {
            return;

        }

        currentInteractable = interactable;
        DestinationPosition = currentInteractable.interactionLocation.position;

        agent.SetDestination(DestinationPosition);

        agent.Resume();

    }
    

    private IEnumerator WaitForInteraction ()
    {
        handleInput = false;

        yield return inputHoldWait;
        while (animator.GetCurrentAnimatorStateInfo(0).tagHash != hashLocomotionTag)
        {
            yield return null;

        }



        handleInput = true;

    }
}
