﻿using UnityEngine;
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
        [SerializeField]
        Transform lookAt;
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
       // cam is used to manipulate any features exclusive to the Camera functions. 
        Camera cam;
        Vector3 offset;


        //This is for the 3rd person control clamp
        const float Y_ANGLE_MIN = 0.0F;
        const float Y_ANGLE_MAX = 50.0F;
        //PartyFormation and perhaps other interface control clamps
        const float X_ANGLE_MIN = 0.0F;
        const float X_ANGLE_MAX = 60.0F;
        [SerializeField]
        float distance = 5f;
        float currentX;
        float currentY;
     
        //I don't remember what the fuck this is.
        //float sensitivityX = 4f;
        //float sensitivityy = 2f;
        float desiredAngle;
        Vector3 desiredTarget;

        //int[] distances = new int[4] { 2, 5, 7, 10 }; // declare numbers as an int array of any size

       
        void Start()
        {
           // camTransform = transform;
            float desiredTargetX = currentX;
            float desiredTargetY = currentY;
            cam = GetComponent<Camera>();
        }        
        private void Update()
        {     
            camControl();
            checkForTargets();
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
                playerCamInputs();
            }

            if (Input.GetButton("rightJoystickButton"))
            {
                camZoom();

                // camTransform.forward = lookAt.transform.forward;

                //currentY = lookAt.position.y;
                // offset = transform.position - lookAt.position;
            }
        }

        private void playerCamInputs()
        {
            currentX += Input.GetAxis("rightJoystickHorizontal");
            currentY += Input.GetAxis("rightJoystickVertical");
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);         
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
            camTransform.forward = lookAt.transform.forward;
            desiredAngle = lookAt.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

            //Breaks the game.
            //for (int i = 0; (Input.GetButton("rightJoystickButton")); i++)
            //{                                                       
            //    switch (i)
            //    {
            //        case 1:
            //            distance = 3f;
            //            break;
            //        case 2:
            //            distance = 5f;
            //            break;
            //        case 3:
            //            distance = 7f;
            //            break;
            //        case 4:
            //            distance = 10f;
            //            break;
            //        default:
            //            distance = 7f;
            //            break;
            //    }
            //}         
            //   camTransform.forward = lookAt.transform.forward;
            //currentX = lookAt.position.x;
        }
        void LateUpdate()
        {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rotation * dir;
            camTransform.LookAt(lookAt.position);
            //If player camera input is 0,0 then maybe do something
            //if (currentX == 0 && currentY == 0){}          
        }
    }
