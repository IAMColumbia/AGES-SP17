using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        public int m_PlayerNumber = 1;
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;
        //private string horizontalInput;
        //private string verticalInput;
        private string crouchInput;
        private string jumpInput;
        // the world-relative desired move direction, calculated from the camForward and user input.

        //Copied from Tank movement  Ctrl+F "Bacon"
        //Combining tankmovement script to third person user control

        private Rigidbody m_Rigidbody;
        private float m_MovementInputValue;

        static void playerNumber(int m_PlayerNumber)
        {
            m_PlayerNumber *= m_PlayerNumber;
        }
        private void Start()
        {
            //Bacon

      //      public string playerPrefix = "P1_";

 
   //  bool is_jump = Input.GetButtonDown(playerPrefix + "Jump");
    
 
            m_Character = GetComponent<ThirdPersonCharacter>();
            
            //horizontalInput = "Horizontal" + m_PlayerNumber;
            //verticalInput = "Vertical" + m_PlayerNumber;
            crouchInput = "Crouch" + m_PlayerNumber;
           
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
         
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetButton("Jump" + m_PlayerNumber);
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            //Replacing user control here.
            //float h = CrossPlatformInputManager.GetAxis(HorizontalMovementAxisName);
            //float v = CrossPlatformInputManager.GetAxis(VerticalMovementAxisName);

            float h = Input.GetAxis("Horizontal" + m_PlayerNumber);
            float v = Input.GetAxis("Vertical" + m_PlayerNumber);
            bool crouch = Input.GetButton(crouchInput);
            // calculate move direction to pass to character
            if (m_Cam != null)
            {
            //    // calculate camera relative direction to move:
                  m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                  m_Move = v*m_CamForward + h*m_Cam.right;
            }
           
           
                // we use world-relative directions in the case of no main camera
                else{


                     m_Move = v * Vector3.forward + h * Vector3.right;
                     Debug.Log("Should be moving");
                 }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }


