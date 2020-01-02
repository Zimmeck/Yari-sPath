using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorScr : MonoBehaviour
{
    public bool openDoor;
    public bool withSpear;
    public GameObject buttonSprite;

    public GameObject door;
    public Animator myAnimator;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            buttonSprite.GetComponent<SpriteRenderer>().color = Color.green;
            door.GetComponent<Door>().OpenDoor();
            myAnimator.SetBool("Pushed", true);
        }
        else
        {
            buttonSprite.GetComponent<SpriteRenderer>().color = Color.red;
            door.GetComponent<Door>().CloseDoor();
            myAnimator.SetBool("Pushed", false);
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("SpearL") || collision.CompareTag("SpearR") || collision.CompareTag("SpearU"))
        {
            openDoor = true;
            withSpear = true;
        }
        else
        {
            withSpear = false;
        }


        if (collision.GetComponentInParent<CanPushButton>() != null)
        {
            if (!withSpear)
            {
                openDoor = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpearL") || collision.CompareTag("SpearR") || collision.CompareTag("SpearU"))
        {
            openDoor = false;
        }

        if (collision.GetComponentInParent<CanPushButton>() != null)
        {
            if (!withSpear)
            {
                openDoor = false;
            }
        }
    }
}
