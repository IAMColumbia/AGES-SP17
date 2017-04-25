using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnitySampleAssets.Characters.ThirdPerson;

    public class BasicCamera : MonoBehaviour
    {
        // Use this for initialization

        IActivatable objectLookedAt;       
        [SerializeField]
        LayerMask layerActivatableObjectsAreOn;  
        //Refactor so lookAt is temporary targeting system    
        [SerializeField]
        Transform lookAt;
        [SerializeField]
        Transform player;
        [SerializeField]
        Transform TargetPopulator;
        //CamTransform is used to control the camera's positioning and movement.
        [SerializeField]
        Transform camTransform;
        //Use panel activity to control camera controls.
        [SerializeField]
        GameObject mainEnemy;
        [SerializeField]
        GameObject pauseMenuPanel;
        //Ray used to determine if an object can be activated. 
        [SerializeField]
        float maxDistanceToActivateObjects = 3;
        [SerializeField]
        float rotationDamping;
    // cam is used to manipulate any features exclusive to the Camera functions. 
        Camera cam;
        Vector3 cameraCurrentPosition; //Camera at
        Vector3 cameraNewPosition;     //Camera to
        Vector3 cameraOffset;           //distance from player at all times
      
        public float autoSpeed = 60F;
        private float startTime;
        float journeyLength;
    //This is for the 3rd person control clamp
        const float X_ANGLE_MIN = -90F;
        const float X_ANGLE_MAX = 90F;
        const float Y_ANGLE_MIN = -30F;
        const float Y_ANGLE_MAX = 30F;

        const float Z_ANGLE_MIN = -90F;
        const float Z_ANGLE_MAX = 90F;

    public float
      clampMarginMinX = 0.0f,
      clampMarginMaxX = 0.0f,
      clampMarginMinY = 0.0f,
      clampMarginMaxY = 0.0f;
    float speed = 0.0f;

    // The minimum and maximum values which the object can go
    private float
        m_clampMinX,
        m_clampMaxX,
        m_clampMinY,
        m_clampMaxY;

    //Dir variables for cameraNewPosition.
    [SerializeField]
        float cameraAngle = 5f;
        [SerializeField]
        float cameraDistance = 5f; //z variable
        //Player Movement Input
        float m_HorizontalInputValue;
        float m_VerticalInputValue;
        float anyPlayerMovementInput;
        //Player Camera Movement Input
        float currentX;
        float currentY;
        float currentZ;
        float anyPlayerCameraMovementInput;
        float desiredAngle;
        bool isAlive;
        Vector3 desiredTarget;
    public int m_PlayerNumber = 1;
    void Start()
        {
           // camTransform = transform;
           
            cam = GetComponent<Camera>();
        //Keep player Object in camera view variables
        m_clampMinX = Camera.main.ScreenToWorldPoint(new Vector2(0 + clampMarginMinX, 0)).x;
        m_clampMaxX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - clampMarginMaxX, 0)).x;
        m_clampMinY = Camera.main.ScreenToWorldPoint(new Vector2(0, 0 + clampMarginMinY)).y;
        m_clampMaxY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height + clampMarginMaxY)).y;

    }        
        private void Update()
        {
       
       
        checkForTargets();
        CamPlayerLock();
    }
    void LateUpdate()
    {
        
       

      camBalance();
        //camZoom();          
    }
    private void camBalance()
    {
        cameraNewPosition = player.transform.position - player.transform.forward * 10.0f + Vector3.up * 5f;
        float bias = 0.96f;
        transform.position = transform.position * bias + cameraNewPosition * (1.0f-bias);
        transform.LookAt(player.transform.position + player.transform.forward * 30.0f);
    }
    private void camControl()
        {              
          
            if (!pauseMenuPanel.activeSelf)
            {
               
            }
            if (Input.GetButton("Start"))
            {
                camZoom();
                camTransform.forward = lookAt.transform.forward;
                currentY = lookAt.position.y;
                cameraOffset = transform.position - lookAt.position;
            }
        }
  
    private void checkForTargets()
        {           
            RaycastHit hit;
            Vector3 endPoint = transform.position + maxDistanceToActivateObjects * transform.forward;
          
            Ray ray = cam.ScreenPointToRay(new Vector3(250, 250, 0));
           
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                objectLookedAt = hit.collider.gameObject.GetComponent<IActivatable>();
                targetActivatable();
                // Do something with the object that was hit by the raycast.
            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        }
    private void targetActivatable()
        {
            if (gameObject.tag == "Enemy")
            {

            }
            else if (gameObject.tag == "Item")
            {

            }
            else if (gameObject.tag == "object")
            {

            }
            else
            {

            }
        }
    private void camZoom()
        {
            camTransform.forward = player.transform.forward;
            desiredAngle = player.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle * rotationDamping, 0);
      
    }        
    private void CamPlayerLock()
    {
       
    Vector3 direction = Vector3.zero;
        // Going left
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.right * -1;
        }
        // Going right
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;

        }
        //going down
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
        }
        // Going up
        else if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
        }

        if (player.transform.position.x < m_clampMinX)
        {
            // If the object position tries to exceed the left screen bound clamp the min x position to 0.
            // The maximum x position won't be clamped so the object can move to the right.
            direction.x = Mathf.Clamp(direction.x, 0, Mathf.Infinity);
        }

        if (player.transform.position.x > m_clampMaxX)
        {
            // Same goes here
            direction.x = Mathf.Clamp(direction.x, Mathf.NegativeInfinity, 0);

        }
        if (player.transform.position.y < m_clampMinY)
        {
            // If the object position tries to exceed the left screen bound clamp the min x position to 0.
            // The maximum x position won't be clamped so the object can move to the right.
            direction.x = Mathf.Clamp(direction.y, 0, Mathf.Infinity);
        }
        if (player.transform.position.y > m_clampMaxY)
        {
            // Same goes here
            direction.y = Mathf.Clamp(direction.y, Mathf.NegativeInfinity, 0);
        }

        transform.position += direction * (Time.deltaTime * speed);
    }
}
          

