using UnityEngine;
using System.Collections;

public class FootSoldierMovement : MonoBehaviour 
{
    #region Editor Fields
    [SerializeField]
    FootSoldierController footSoldierController;

    [SerializeField]
    Rigidbody movementRigidBody;

    [SerializeField]
    float speed = 5;

    #endregion

    #region Fields
    private float xInput;
    private float yInput;
    private Vector3 moveDirection;
    #endregion

    #region Monobehaviour functions
    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        // We aren't going to do this for now. Only change if camera needs to move.
        //ConvertInputToCameraRelative();
        UpdateMovement();
        UpdateRotation();
    }
    #endregion

    private void GetInput()
    {
        xInput = Input.GetAxis("HorizontalP" + footSoldierController.ControllingPlayer.PlayerNumber);
        yInput = Input.GetAxis("VerticalP" + footSoldierController.ControllingPlayer.PlayerNumber);

        moveDirection = new Vector3(xInput, 0, yInput);
    }

    private void ConvertInputToCameraRelative()
    {
        // We aren't going to do this for now. Only change if camera needs to move.
        moveDirection = Camera.main.transform.InverseTransformDirection(moveDirection);
    }

    private void UpdateRotation()
    {
        // Debug.DrawRay(rigidbody.position, moveDirection * 3, Color.red);
        float turnThreshold = 0.1f;
        if (moveDirection.magnitude > turnThreshold)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            newRotation.eulerAngles = new Vector3(0, newRotation.eulerAngles.y, 0);
           movementRigidBody.transform.rotation = newRotation;
        }
    }

    private void UpdateMovement()
    {
        movementRigidBody.MovePosition(movementRigidBody.position + (moveDirection * speed * Time.deltaTime));
    }
}
