using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class AICharacter : MonoBehaviour 
{
    [SerializeField]
    Transform pointToNavigateToward;

    CharacterController characterToControl;

    void Start()
    {
        characterToControl = GetComponent<CharacterController>();
    }
    
	void Update() 
	{
        Vector3 towardDestination =  (pointToNavigateToward.position - characterToControl.transform.position).normalized;

        MoveCommand move = new MoveCommand(towardDestination.x * Time.deltaTime, towardDestination.y * Time.deltaTime);
        move.Execute(characterToControl);
	}
}
