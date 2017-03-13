using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public float inputHoldDelay = 0.5f;
    public float turnSpeedThreshold = 0.5f;

    public float speedDampTime = 0.1f; //used to smooth speed changes
    public float slowingSpeed = 0.175f; //how fast to move the character towards target when slowing
    public float turnSmoothing = 15f; //the higher the number, the slower the turn (and vice versa)


    WaitForSeconds inputHoldWait;
    Vector3 destinationPosition;
    Interactable currentInteractable;
    bool handleInput = true;


    const float stopDistanceProportion = 0.1f; //10% of stopping distance
    const float navMeshSampleDistance = 4f; //the distance away from the click that the navMesh can be

    //readonly: like a "get-only" variable
    private readonly int hashSpeedParameter = Animator.StringToHash("Speed");
    private readonly int hashLocomotionTag = Animator.StringToHash("Locomotion");

    private void Start()
    {
        agent.updateRotation = false;

        inputHoldWait = new WaitForSeconds(inputHoldDelay);
        destinationPosition = transform.position;
    }

    private void OnAnimatorMove()
    {
        //animator.deltaPosition = how far the NavMeshAgent wants to move this frame
        //so, what we're saying is: velocity = distance / time;
        agent.velocity = animator.deltaPosition / Time.deltaTime;
    }

    private void Update()
    {
        //if the agent doesn't currently have a path
        if (agent.pathPending)
            return;

        float speed = agent.desiredVelocity.magnitude;

        //if the amount the agent has left to move is less than or equal to 10% of the stopping distance
        if(agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
        {
            Stopping(out speed);
        }
        //if the amount the agent has left to move is less than the stopping distancce (middle ring)
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Slowing(out speed, agent.remainingDistance);
        }
        //when we're going a quarter as fast as we want to we start turning
        else if (speed > turnSpeedThreshold)
        {
            Moving();
        }

        animator.SetFloat(hashSpeedParameter, speed, speedDampTime, Time.deltaTime);

    }

    //called when you're REALLY close
    private void Stopping(out float Speed)
    {
        agent.Stop(); //stop the NavMeshAgent
        transform.position = destinationPosition; //snap player to position.  Should jerk because this distance should be small
        Speed = 0f; //stop the speed

        if(currentInteractable)
        {
            transform.rotation = currentInteractable.interactionLocation.rotation; //interactionLocation is a transform attached to the object
            currentInteractable.Interact();
            currentInteractable = null;
            StartCoroutine(WaitForInteraction());
        }
    }

    private void Slowing(out float speed, float distanceToDestination)
    {
        agent.Stop(); //stop the NavMeshAgent so we have more control over it
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);

        //the closer the agent is to the destination, the smaller the number will be
        //the farther the agent is to the destination, the larger the number will be
        //we subtract by 1 to invert that (and distanceToDestination / agent.stoppingDistance WILL be a small number)
        float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;

        //using proportionalDistance,
        //if the agent is far from the target, the amount moved per frame will be slower (to allow more time to move)
        //if the agent is near the target, the amount of change will be faster (to allow less time to move)
        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);

        //if we have a currentInteractable, get its rotation. Otherwise, get our rotation
        Quaternion targetRotation = currentInteractable ? currentInteractable.interactionLocation.rotation : transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);
    }

    private void Moving()
    {
        //we don't want to mess with the movement at all (we want to leave it).  So, we're only going to change the rotation
        Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);

    }

    //we'll call this from the even trigger
    public void OnGroundClick(BaseEventData data) //BaseEventData is whatever information is carried with the event
    {
        if (!handleInput)
            return;

        currentInteractable = null;

        PointerEventData pointerData = (PointerEventData)data; //we don't want ALL the data, we just want the pointer data
        NavMeshHit hit; //acts like a raycast, but for navmeshes!

        //pointerData.pointerCurrentRaycast.worldPosition = the position in the world where it was clicked (on the meshCollider)  When you do click something, it generates an event.  We use that to get the position
        //returns this info to the hit variable
        //the distance that we'll sample over
        //what NavMesh "layer" we want the sample to be over
        if (NavMesh.SamplePosition
            (pointerData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            destinationPosition = hit.position;
        }
        else
        {
            //find your way to where the user clicked
            destinationPosition = pointerData.pointerCurrentRaycast.worldPosition;
        }

        agent.SetDestination(destinationPosition);
        agent.Resume();
    }

    public void OnInteractableClick(Interactable interactable)
    {
        if (!handleInput)
            return;

        currentInteractable = interactable;
        destinationPosition = currentInteractable.interactionLocation.position;

        agent.SetDestination(destinationPosition);
        agent.Resume();
    }

    private IEnumerator WaitForInteraction()
    {
        handleInput = false;

        yield return inputHoldWait;

        //while our current state does not have a locomotion tag, wait
        while(animator.GetCurrentAnimatorStateInfo(0).tagHash != hashLocomotionTag)
        {
            yield return null; //wait for end of frame
        }

        handleInput = true;
    }

}
