using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public int PointsPickedUp;
	public GameObject point;
	public GameObject pointParticleSystem;
	public GameObject powerUp;
	public GameObject powerUpParticleSystem;
	public bool poweredUp;
	[SerializeField]
	private float widthRange;
	[SerializeField]
	private float depthRange;

    public GameObject SoundEffect;
    [SerializeField]
    private AudioClip powerUpClip;
    [SerializeField]
    private AudioClip pointClip;
    private AudioSource source;


	// Use this for initialization
	void Start () {
	
		poweredUp = false;
		PointsPickedUp = 0;

        source = SoundEffect.GetComponent<AudioSource>();

        //Vector3 position1 = new Vector3(Random.Range(-widthRange, widthRange), 5f, Random.Range(-depthRange, depthRange));
        //Instantiate(point, position1, Quaternion.identity);

        //Vector3 position2 = new Vector3(Random.Range(-widthRange, widthRange), 5f, Random.Range(-depthRange, depthRange));
        //Instantiate(powerUp, position2, Quaternion.identity);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider obj)
	{
		if(obj.tag == "Point")
		{
			PointsPickedUp += 1;
			poweredUp = false;

            source.PlayOneShot(pointClip, 1f);
            Instantiate (pointParticleSystem, obj.transform.position, Quaternion.identity);
			//Destroy (pointParticleSystem.gameObject, 1f);
			Destroy (obj.gameObject);
			Vector3 position = new Vector3 (Random.Range(-widthRange,widthRange), 5f, Random.Range(-depthRange,depthRange));
			Instantiate (point, position, Quaternion.identity);
		}

		if(obj.tag == "PowerUp")
		{
			poweredUp = true;

            source.PlayOneShot(powerUpClip, 1f);
			Instantiate (powerUpParticleSystem, obj.transform.position, Quaternion.identity);
			Destroy (obj.gameObject);
			Vector3 position = new Vector3 (Random.Range(-widthRange,widthRange), 5f, Random.Range(-depthRange,depthRange));
			Instantiate (powerUp, position, Quaternion.identity);
		}
	}
}
