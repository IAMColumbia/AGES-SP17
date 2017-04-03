using UnityEngine;
using System.Collections;

public class SheildScript : MonoBehaviour {
    [SerializeField]
    int damagePoints;
	// Use this for initialization
	void Awake ()
    {
        damagePoints = 4;
	}
	
	// Update is called once per frame
	void Update ()
    {
        DestroySheild();
	}
    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "EnemySpell")
        {
            Debug.Log("FSDGHASKGA");
            damagePoints = damagePoints - 1;
        }
    }
    void DestroySheild()
    {
        if(damagePoints <=0)
        {
            Destroy(gameObject);
        }
    }
}
