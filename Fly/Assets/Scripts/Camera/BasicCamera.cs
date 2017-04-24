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

    [SerializeField]
        float cameraDistance = 5f;
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
            float desiredTargetX = currentX;
            float desiredTargetY = currentY;
            cam = GetComponent<Camera>();
        }        
        private void Update()
        {
        m_HorizontalInputValue = Input.GetAxis("Horizontal" + m_PlayerNumber);
        m_VerticalInputValue = Input.GetAxis("Vertical" + m_PlayerNumber);
        anyPlayerMovementInput = m_HorizontalInputValue + m_VerticalInputValue;
        anyPlayerCameraMovementInput = currentX + currentY;                
        checkForTargets();
        }
    void LateUpdate()
    {
        
        Vector3 dir = new Vector3(0, 0, -cameraDistance);
        Quaternion cameraMovementRotation = Quaternion.Euler(player.transform.rotation.x, player.transform.rotation.y, 0);
        cameraNewPosition = player.transform.position + cameraMovementRotation * dir;
        transform.position = Vector3.MoveTowards(transform.position, cameraNewPosition, cameraDistance);      
        if (anyPlayerMovementInput != 0)
         
        {
            camZoom();
            //  camControl();
            //camTransform.position = lookAt.position + cameraMovementRotation * dir;
            // camTransform.transform.Rotate(lookAt.rotation.x * Time.deltaTime, lookAt.rotation.y * Time.deltaTime, lookAt.rotation.z * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraMovementRotation, Time.deltaTime * rotationDamping);
        }
        else if (anyPlayerCameraMovementInput != 0)
        {
            camBalance();
            //camTransform.LookAt(lookAt.position);
           // camTransform.transform.Rotate(player.rotation.x * Time.deltaTime, player.rotation.y * Time.deltaTime, lookAt.rotation.z * Time.deltaTime);
        }
        else
        {
            camBalance();
        }
                          
        //If player camera input is 0,0 then maybe do something
       
        //if(mainEnemy.range > 20)
        //{

        //}
    }
    private void camBalance()
    {
        cameraCurrentPosition = transform.position;
        Quaternion balancedRotation = Quaternion.Euler(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z);
        Quaternion currentRotation = transform.rotation;
        journeyLength = Vector3.Distance(cameraCurrentPosition, cameraNewPosition);
        float distCovered = (Time.time - startTime) * autoSpeed;
        float fracJourney = distCovered / journeyLength;
        startTime = Time.time;
        Quaternion autoRotation = Quaternion.Lerp(currentRotation, balancedRotation, fracJourney);

        if (isAlive && anyPlayerMovementInput == 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, balancedRotation, distCovered);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, distCovered);
            //currentX = Mathf.Clamp(player.transform.rotation.x, X_ANGLE_MIN, X_ANGLE_MAX);
            //currentY = Mathf.Clamp(player.transform.position.y, Y_ANGLE_MIN, Y_ANGLE_MAX);
            currentZ = Mathf.Clamp(player.transform.rotation.z, Z_ANGLE_MIN, Z_ANGLE_MAX);
        }
    }
    private void camControl()
        {              
            if (mainEnemy.activeSelf)
            {
                currentY = Mathf.Clamp(mainEnemy.transform.position.y + 4, Y_ANGLE_MIN, Y_ANGLE_MAX);
                currentX = Mathf.Clamp(mainEnemy.transform.position.y + 4, X_ANGLE_MIN, X_ANGLE_MAX);                
            }
                      
            if (!pauseMenuPanel.activeSelf)
            {
                //playerCamInputs();
            }
            if (Input.GetButton("Start"))
            {
                camZoom();
                camTransform.forward = lookAt.transform.forward;
                currentY = lookAt.position.y;
                cameraOffset = transform.position - lookAt.position;
            }
        }
        //private void playerCamInputs()
        //{
        //    currentX += Input.GetAxis("rightJoystickHorizontal");
        //    currentY += Input.GetAxis("rightJoystickVertical");
        //    currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);         
        //}      
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
    
          
    }

