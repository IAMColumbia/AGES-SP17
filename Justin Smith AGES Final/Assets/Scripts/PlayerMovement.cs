using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 6f;

    Vector3 movement;
    Rigidbody playerRigidbody;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        // Store the input axes.
        float moveHorizontal = Input.GetAxisRaw("mHorizontal");
        float moveVertical = Input.GetAxisRaw("mVertical");
        float dirHorizontal = Input.GetAxisRaw("dHorizontal");
        float dirVertical = Input.GetAxisRaw("dVertical");

        // Move the player around the scene.
        Move(moveHorizontal, moveVertical);

        // Turn the player to face the joystick input.
        Turning(dirHorizontal, dirVertical);

        // Animate the player.
        //Animating(horizontal, vertical);
    }

    void Move(float horizontal, float vertical)
    {
        // Set the movement vector based on the axis input.
        movement.Set(horizontal, 0f, vertical);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning(float horizontal, float vertical)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, transform.eulerAngles.z);
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        //Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        //RaycastHit floorHit;

        //// Perform the raycast and if it hits something on the floor layer...
        //if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        //{
        //    // Create a vector from the player to the point on the floor the raycast from the mouse hit.
        //    Vector3 playerToMouse = floorHit.point - transform.position;

        //    // Ensure the vector is entirely along the floor plane.
        //    playerToMouse.y = 0f;

        //    // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        //    Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

        //    // Set the player's rotation to this new rotation.
        //    playerRigidbody.MoveRotation(newRotatation);
        //}
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
