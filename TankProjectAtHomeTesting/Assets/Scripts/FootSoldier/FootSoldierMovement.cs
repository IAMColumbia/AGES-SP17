using UnityEngine;
using System.Collections;

public class FootSoldierMovement : MonoBehaviour 
{
    #region Editor Fields
    [SerializeField]
    FootSoldierController footSoldierController;

    [SerializeField]
    Rigidbody movementRigidBody;

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
        ConvertInputToCameraRelative();
        UpdateMovement();
        UpdateRotation();
    }
    #endregion

    private void GetInput()
    {
        xInput = Input.GetAxis("HorizontalP" + footSoldierController.ControllingPlayer.PlayerNumber);
        yInput = Input.GetAxis("VerticalP" + footSoldierController.ControllingPlayer.PlayerNumber);
    }

    private void ConvertInputToCameraRelative()
    {
        moveDirection = new Vector3(yInput, 0, -xInput);
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
            transform.rotation = newRotation;
        }
    }

    private void UpdateMovement()
    {
        float speed = 3;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (moveDirection * speed * Time.deltaTime));
    }
}
