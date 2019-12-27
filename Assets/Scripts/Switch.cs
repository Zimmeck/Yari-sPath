using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private float enterSpearPos;
    private float exitSpearPos;
    private GameObject switchPlatformsParent;
    static public bool switchedToRight;
    private float blockSwitchTimer;
    public float startBlockSwitchTimer;
    public GameObject door;

    void ActivateRightPlatforms()
    {
        for (int i = 0; i < switchPlatformsParent.transform.childCount; i++)
        {
            if (switchPlatformsParent.transform.GetChild(i).GetComponent<SwitchLefPlatform>() != null)
            {
                //switchPlatformsParent.transform.GetChild(i).gameObject.SetActive(false);
                switchPlatformsParent.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                switchPlatformsParent.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;

            }

            if (switchPlatformsParent.transform.GetChild(i).GetComponent<SwitchRightPlatform>() != null)
            {
                //switchPlatformsParent.transform.GetChild(i).gameObject.SetActive(true);
                switchPlatformsParent.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                switchPlatformsParent.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        blockSwitchTimer = startBlockSwitchTimer;
    }

    void ActivateLeftPlatforms()
    {
        for (int i = 0; i < switchPlatformsParent.transform.childCount; i++)
        {
            if (switchPlatformsParent.transform.GetChild(i).GetComponent<SwitchLefPlatform>() != null)
            {
                //switchPlatformsParent.transform.GetChild(i).gameObject.SetActive(true);
                switchPlatformsParent.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                switchPlatformsParent.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }

            if (switchPlatformsParent.transform.GetChild(i).GetComponent<SwitchRightPlatform>() != null)
            {
                //switchPlatformsParent.transform.GetChild(i).gameObject.SetActive(false);
                switchPlatformsParent.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                switchPlatformsParent.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        blockSwitchTimer = startBlockSwitchTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        switchPlatformsParent = GameObject.Find("SwitchPlatformsParent");


        if (switchedToRight)
        {
            ActivateRightPlatforms();
        }
        else
        {
            ActivateLeftPlatforms();
        }

    }

    // Update is called once per frame
    void Update()
    {
        blockSwitchTimer -= Time.deltaTime;

        if (switchedToRight)
        {
            GetComponent<Animator>().SetBool("ToRight", true);

            if (door != null)
            {
                door.GetComponent<Door>().OpenDoor();
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("ToRight", false);

            if (door != null)
            {
                door.GetComponent<Door>().CloseDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<SpearScr>() != null){
            enterSpearPos = collision.transform.position.x;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<SpearScr>() != null)
        {
            exitSpearPos = collision.transform.position.x;

            if (enterSpearPos > exitSpearPos)
            {
                switchedToRight = false;
                //logica encender left platformas
                ActivateLeftPlatforms();
            }

            if (enterSpearPos < exitSpearPos)
            {
                switchedToRight = true;
                ActivateRightPlatforms();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                if (blockSwitchTimer <= 0)
                {
                    if (switchedToRight)
                    {
                        ActivateLeftPlatforms();
                        switchedToRight = false;
                    }
                    else
                    {
                        ActivateRightPlatforms();
                        switchedToRight = true;
                    }
                }
            }
        }
    }
}
