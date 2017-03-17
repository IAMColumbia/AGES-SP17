using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    public NavMeshAgent Agent;
    public float inputHoldDelay = 0.5f;
    public float turnSpeedThreshold = 0.5f;
    public float speedDampTime = 0.1f;
    public float slowingSpeed = 0.175f;
    public float turnSmoothing = 15f;

    private WaitForSeconds inputHoldWait;
    private Vector3 destinationPosition;
    private Interactable currentInteractable;
    private bool handleInput = true;

    private const float stopDistanceProportion = 0.1f;
    private const float navMeshSampleDistance = 4f;

    private readonly int hashSpeedPara = Animator.StringToHash("Speed");
    private readonly int hashLocomotionTag = Animator.StringToHash("Locomotion");

    private void Start()
    {
        Agent.updateRotation = false;

        inputHoldWait = new WaitForSeconds(inputHoldDelay);

        destinationPosition = transform.position;

    }

    private void OnAnimatorMove()
    {
        Agent.velocity = animator.deltaPosition / Time.deltaTime;
    }

    private void Update()
    {
        if (Agent.pathPending)
        {
            return;
        }

        float speed = Agent.desiredVelocity.magnitude;

        if (Agent.remainingDistance <= Agent.stoppingDistance * stopDistanceProportion)
        {
            Stopping(out speed);
        }

        else if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            Slowing(out speed, Agent.remainingDistance);
        }

        else if (speed > turnSpeedThreshold)
        {
            Moving();
        }

        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);



    }

    private void Stopping(out float speed)
    {
        Agent.Stop();
        transform.position = destinationPosition;
        speed = 0f;

        if(currentInteractable)
        {
            transform.rotation = currentInteractable.interactionLocation.rotation;
            currentInteractable.Interact();
            currentInteractable = null;
            StartCoroutine(WaitForInteraction());
        }
    }

    private void Slowing(out float speed, float distanceToDestination)
    {
        Agent.Stop();
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);

        float proportionalDistance = 1f - distanceToDestination / Agent.stoppingDistance;
        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);

        Quaternion targetRotation = currentInteractable ? currentInteractable.interactionLocation.rotation : transform.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);
    }

    private void Moving()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Agent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    }

    public void OnGroundClick(BaseEventData data)
    {
        if(!handleInput)
        {
            return;
        }

        currentInteractable = null;

        PointerEventData pData = (PointerEventData)data;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            destinationPosition = hit.position;
        }
        else
        {
            destinationPosition = pData.pointerCurrentRaycast.worldPosition;
        }

        Agent.SetDestination(destinationPosition);
        Agent.Resume();
    }

    public void OnInteractableClick(Interactable interactable)
    {
        if(!handleInput)
        {
            return;
        }

        currentInteractable = interactable;
        destinationPosition = currentInteractable.interactionLocation.position;
        Agent.SetDestination(destinationPosition);
        Agent.Resume();
    }

    private IEnumerator WaitForInteraction()
    {
        handleInput = false;

        yield return inputHoldWait;

        while(animator.GetCurrentAnimatorStateInfo(0).tagHash != hashLocomotionTag)
        {
            yield return null;
        }

        handleInput = true;
    }
}
