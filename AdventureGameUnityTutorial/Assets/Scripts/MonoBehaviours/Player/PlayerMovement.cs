using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Animator Animator;
    public NavMeshAgent Agent;
    public float InputHoldDelay = 0.5f;
    public float TurnSpeedThreshold = 0.5f;
    public float SpeedDampTime = 0.1f;
    public float SlowingSpeed = 0.175f;
    public float TurnSmoothing = 15f;

    WaitForSeconds inputHoldWait;
    Vector3 destinationPosition;
    Interactable currentInteractable;

    bool handleInput = true;

    const float stopDistanceProportion = 0.1f;
    const float navMeshSampleDistance = 4f;

    readonly int hashSpeedPara = Animator.StringToHash("Speed");
    readonly int hashLocomotionTag = Animator.StringToHash("Locomotion");

    private void Start()
    {
        Agent.updateRotation = false;

        inputHoldWait = new WaitForSeconds(InputHoldDelay);

        destinationPosition = transform.position;
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
        else if (speed > TurnSpeedThreshold)
        {
            Moving();
        }

        Animator.SetFloat(hashSpeedPara, speed, SpeedDampTime, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        Agent.velocity = Animator.deltaPosition / Time.deltaTime;
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
        transform.position = 
            Vector3.MoveTowards(transform.position, destinationPosition, SlowingSpeed * Time.deltaTime);

        float proportionalDistance = 1f - distanceToDestination / Agent.stoppingDistance;
        speed = Mathf.Lerp(SlowingSpeed, 0f, proportionalDistance);

        Quaternion targetRotation = currentInteractable ? currentInteractable.interactionLocation.rotation : transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);

    }

    private void Moving()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Agent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, TurnSmoothing);
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

        if (NavMesh.SamplePosition
            (pData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
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

        while(Animator.GetCurrentAnimatorStateInfo(0).tagHash != hashLocomotionTag)
        {
            yield return null;
        }

        handleInput = true;
    }
}
