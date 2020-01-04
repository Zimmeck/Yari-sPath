using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioPlayer myAP;

    public Transform openPosition;
    public Transform closePosition;
    private float timeForCameraShake;

    public float speed;

    public bool opened;

    public void OpenDoor()
    {
        transform.position = Vector2.MoveTowards(transform.position, openPosition.position, speed * Time.deltaTime);
    }

    public void CloseDoor()
    {

        transform.position = Vector2.MoveTowards(transform.position, closePosition.position, speed * Time.deltaTime);
    }

    private void Awake()
    {
        myAP = GetComponent<AudioPlayer>();
    }

    private void Start()
    {

        closePosition.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, openPosition.position) <= 0.05f)
        {
            if (!opened)
            {


                Camera.main.gameObject.GetComponent<CameraBehaviour>().DoCameraShake();
                opened = true;
            }
        }

        if (Vector2.Distance(transform.position, closePosition.position) <= 0.05f)
        {
            if (opened)
            {
                Camera.main.gameObject.GetComponent<CameraBehaviour>().DoCameraShake();
                opened = false;
            }
        }


        if (opened)
        {
            if (transform.position != openPosition.position)
            {
                myAP.PlayAudioSourceLoop();
            }
            else
            {
                myAP.StopAudioSourceLoop();
            }
        }
        else
        {
            if (transform.position != closePosition.position)
            {
                myAP.PlayAudioSourceLoop();
            }
            else
            {
                myAP.StopAudioSourceLoop();
            }
        }
        //if (Vector2.Distance(transform.position, openPosition.position) <= 0.05f)
        //{
        //    if (transform.GetComponentInChildren<PlayerScr>() != null)
        //    {
        //        GameObject playerRef = transform.GetComponentInChildren<PlayerScr>().gameObject;

        //        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
        //        {
        //            Debug.Log("patata");
        //            playerRef.gameObject.transform.parent = null;
        //            playerRef.GetComponent<Rigidbody2D>().gravityScale = 1;
        //        }
        //    }
        //}

        //if (Vector2.Distance(transform.position, closePosition.position) <= 0.05f)
        //{
        //    if (transform.GetComponentInChildren<PlayerScr>() != null)
        //    {
        //        GameObject playerRef = transform.GetComponentInChildren<PlayerScr>().gameObject;

        //        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
        //        {
        //            if (playerRef.GetComponent<Rigidbody2D>().velocity.y > 5f)
        //            {
        //                Debug.Log("patata");
        //                playerRef.gameObject.transform.parent = null;
        //                playerRef.GetComponent<PlayerScr>().jumpSpeed = 15;
        //            }

        //        }
        //    }
        //}
    }
}
