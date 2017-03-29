using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {

    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private GameObject p1;
    [SerializeField]
    private GameObject p2;
    [SerializeField]
    private GameObject p3;
    [SerializeField]
    private GameObject p4;

    [SerializeField]
    private GameObject point;
    [SerializeField]
    private GameObject powerUp;

    [SerializeField]
    private float widthRange;
    [SerializeField]
    private float depthRange;

    private float timePassed;
    [SerializeField]
    private float timeStart;


    // Use this for initialization
    void Start () {

        p1.GetComponent<PlayerMovement>().canPlayerMove = true;
        p2.GetComponent<PlayerMovement>().canPlayerMove = true;
        p3.GetComponent<PlayerMovement>().canPlayerMove = true;
        p4.GetComponent<PlayerMovement>().canPlayerMove = true;

        winPanel.SetActive(false);

        Vector3 position1 = new Vector3(Random.Range(-widthRange, widthRange), 5f, Random.Range(-depthRange, depthRange));
        Instantiate(point, position1, Quaternion.identity);

        Vector3 position2 = new Vector3(Random.Range(-widthRange, widthRange), 5f, Random.Range(-depthRange, depthRange));
        Instantiate(powerUp, position2, Quaternion.identity);

        timePassed = timeStart;

    }

    // Update is called once per frame
    void Update () {

        if (timePassed <= 0f)
        {
            //round is ended
            p1.GetComponent<PlayerMovement>().canPlayerMove = false;
            p2.GetComponent<PlayerMovement>().canPlayerMove = false;
            p3.GetComponent<PlayerMovement>().canPlayerMove = false;
            p4.GetComponent<PlayerMovement>().canPlayerMove = false;

            winPanel.SetActive(true);

        }
        else
        {
            timePassed -= Time.deltaTime;
        }
	
	}
}
