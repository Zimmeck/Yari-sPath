using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private Transform destination;
    public GameObject bluePortal;
    public GameObject orangePortal;
    public GameObject portalEnterEffect;

    public float startTimeToTp;
    public float timeToTp;


    public bool isOrange;
    // Start is called before the first frame update
    void Start()
    {
        startTimeToTp = .3f;

        if (!isOrange)
        {
            destination = orangePortal.transform;
        }
        else
        {
            destination = bluePortal.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeToTp -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CanGoThrowPortals>() != null)
        {
            if (Vector2.Distance(transform.position, collision.transform.position) > 0.3f)
            {
                if (timeToTp <= 0)
                {
                    timeToTp = startTimeToTp;
                    destination.GetComponent<Portal>().timeToTp = startTimeToTp;
                    collision.transform.parent.transform.position = new Vector2(destination.position.x, destination.position.y);
                    Instantiate(portalEnterEffect, transform.position, Quaternion.identity);
                    Instantiate(portalEnterEffect, destination.position, Quaternion.identity);
                }

            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Platform"))
        {

            if (collision.CompareTag("SpearR") || collision.CompareTag("SpearL") || collision.CompareTag("SpearU"))
            {
                if (Vector2.Distance(transform.position, collision.transform.position) > 0.3f)
                {
                    if (timeToTp <= 0)
                    {
                        timeToTp = startTimeToTp;
                        destination.GetComponent<Portal>().timeToTp = startTimeToTp;
                        collision.transform.parent.transform.position = new Vector2(destination.position.x, destination.position.y);
                        Instantiate(portalEnterEffect, transform.position, Quaternion.identity);
                        Instantiate(portalEnterEffect, destination.position, Quaternion.identity);
                    }
                }
            }
        }
    }
}
