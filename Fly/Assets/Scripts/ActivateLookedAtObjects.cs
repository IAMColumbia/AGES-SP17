using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace UnitySampleAssets.Characters.ThirdPerson
{
    public class ActivateLookedAtObjects : MonoBehaviour
    {

        //This script is intended to be put on the Camera 
        //This script is modified and currently on the ThirdPersonCharacter
        
        Vector3 origin = new Vector3(0f, 0f, 0f);

        [SerializeField]
        GameObject raycastEnhancers;
 
        [SerializeField]
        float maxDistanceToActivateObjects = 30;

        [SerializeField]
        LayerMask layerActivatableObjectsAreOn;

        [SerializeField]
        Text lookedAtObjectText;
        [SerializeField]
        Text userInputText;


       




        // public Animator anim;

        //AudioSource audioSource; 

        // Use this for initialization	

        IActivatable objectLookedAt;
       

      

        //Start game from scratch...no wand
        void Start()
        {
           
     
        }
        // Update is called once per frame
        void Update()
        {
            UpdateLookedAtObjectText();
          
            CheckedForLookedAtOBjectObjects();       
        }
        private void CheckedForLookedAtOBjectObjects()
        {

            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, transform.forward, out raycastHit,
               maxDistanceToActivateObjects, layerActivatableObjectsAreOn))        
            {

                //if our ray hits something, we go into this code block
                objectLookedAt = raycastHit.collider.gameObject.GetComponent<IActivatable>();
              

                if (objectLookedAt == null)
                {
                    // attack();
                     throw new System.Exception(raycastHit.collider.gameObject.name +
                     " MUST have a script that implements IActivatable script attached to it.");
                }

                string objectName = raycastHit.collider.gameObject.name;
                //Debug.Log("Object Looked at " + objectName);

                if (Input.GetButton("xButton"))
                {
                    if (gameObject.tag == "Items")
                    {
                        raycastHit.collider.gameObject.SetActive(false);

                        Debug.Log("The user is looking at an activatable Object, and now has clicked.");
                        objectLookedAt.DoActivate();
                       
                    }
                
                }
            }
            else
            {
                objectLookedAt = null;
               
            }

            Vector3 endPoint = transform.position + maxDistanceToActivateObjects * transform.forward;
            Debug.DrawLine(transform.position, endPoint, Color.red);
        }   
        private void UpdateLookedAtObjectText()
        {
            if (objectLookedAt != null)
            {
                lookedAtObjectText.gameObject.SetActive(true);
                lookedAtObjectText.text = objectLookedAt.DisplayText;             
            }
            else if (objectLookedAt == null)
            {
                lookedAtObjectText.text = "";
                lookedAtObjectText.gameObject.GetComponent<Text>().enabled = true;                
            }
        }
      
    }
}
