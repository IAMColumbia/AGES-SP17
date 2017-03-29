using UnityEngine;
using System.Collections;

public class Hazard : Pickup
{
    public float blinkspeed = 3;
    public bool deactivatesSelf = true;
    Light hazardLight;
	// Use this for initialization
	new void Start ()
    {
        base.Start();

        hazardLight = GetComponent<Light>();
        StartCoroutine("PingPongLight");
	}

    private void OnCollisionEnter(Collision other)
    {
        TankHealth otherTank = other.gameObject.GetComponent<TankHealth>();
        if(otherTank != null)
        {
            otherTank.TakeDamage(9999999);

            if (deactivatesSelf) { gameObject.SetActive(false); }
        }
    }

    IEnumerator PingPongLight()
    {
        //endless loop.  Keep going until you're destroyed!
        while (true)
        {
            hazardLight.intensity = Mathf.PingPong(Time.time * blinkspeed, 5) + 2;
            yield return null;
        }
    }
}
