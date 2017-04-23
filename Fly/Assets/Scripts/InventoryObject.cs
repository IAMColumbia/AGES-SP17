using UnityEngine;
using System.Collections;
using System;

namespace UnitySampleAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(AudioSource))]
    public class InventoryObject : MonoBehaviour, IActivatable
    {
        [SerializeField]
        string displayText;

        [SerializeField]
        string descriptionText;

        [SerializeField]
        string displayCommand;

        [SerializeField]
        GameObject player;

        private string itemName;
        private string itemDescription;
        private int itemID;
        public enum ItemTypes { EQUIPMENT, WEAPON, SCROLL, POTION, CHEST}

        private ItemTypes itemType;

        public string ItemName {
            get { return itemName; }
            set { itemName = value; }
        }
        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public ItemTypes ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }


        bool shouldDisableWhenDonePlayingSoundEffect = false;
        Animator anim;
        AudioSource audioSource;

        //Renderer changeColor;

        //Color colorStart;
        //Color colorEnd;
        float duration = 3;




        public string DescriptionText
        {
            get
            {
                return descriptionText;
            }
        }

        public string DisplayText
        {
            get
            {
                return displayText;
            }
        }

   
       

        public void DoActivate()
        {
            //What happens to inventory Object?
            //Goes away
            //Gets added to inventory list(to do)

            audioSource.Play();
        
            shouldDisableWhenDonePlayingSoundEffect = true;

        }

        // Use this for initialization
        public void Start()
        {
            anim = GetComponent<Animator>();
            //GetComponent<MeshRenderer>().enabled = true;
            //colorStart = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            audioSource = GetComponent<AudioSource>();
        
        }

        // Update is called once per frame
        void Update()
        {

            Rotation();
            if (shouldDisableWhenDonePlayingSoundEffect && !audioSource.isPlaying)
            {
                Debug.Log("Should go away now..");
                gameObject.SetActive(false);
            }
        }
        void InventoryObjects()
        {

        }
        void Rotation()
        {
            transform.Rotate(0, 100 * Time.deltaTime, 0);
            if (shouldDisableWhenDonePlayingSoundEffect)
            {
                transform.Rotate(25, 300 * Time.deltaTime, 100);
                FadeOut();
            }
        }

        public void FadeOut()
        {
            if (shouldDisableWhenDonePlayingSoundEffect && !audioSource.isPlaying)
            {
                float t;
                float alpha = GetComponent<MeshRenderer>().material.color.a;
                for (t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0, t));
                    GetComponent<MeshRenderer>().material.color = newColor;
                }
                gameObject.SetActive(false);

            }
        }

      
    }
}
