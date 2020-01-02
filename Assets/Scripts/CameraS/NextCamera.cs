using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class NextCamera : MonoBehaviour
{
    public GameObject[] virtualCamera;
    private GameObject cameraRef;
    public GameObject frontParallax;
    public GameObject mediumParallax;

    public Transform checkPointPosAlante;
    public Transform checkPointPosAtras;
    public void GoNextCamera()
    {

        //Mover parallax palante
        frontParallax.GetComponent<Parallax>().movingRight = true;
        mediumParallax.GetComponent<Parallax>().movingRight = true;

        frontParallax.GetComponent<Parallax>().timeToStop = frontParallax.GetComponent<Parallax>().startTimeToStop;
        mediumParallax.GetComponent<Parallax>().timeToStop = mediumParallax.GetComponent<Parallax>().startTimeToStop;

        for (int i = 0; i < virtualCamera.Length; i++)
        {
            if (virtualCamera[i].gameObject.activeSelf)
            {
                virtualCamera[i].gameObject.SetActive(false);
                virtualCamera[i + 1].gameObject.SetActive(true);
                cameraRef.GetComponent<CameraBehaviour>().actualCamera = virtualCamera[i + 1].gameObject.gameObject.GetComponent<CinemachineVirtualCamera>();
                break;
            }
        }
    }

    public void GoPreviusCamera()
    {

        //Mover parallax patras
        frontParallax.GetComponent<Parallax>().movingLeft = true;
        mediumParallax.GetComponent<Parallax>().movingLeft = true;

        frontParallax.GetComponent<Parallax>().timeToStop = frontParallax.GetComponent<Parallax>().startTimeToStop;
        mediumParallax.GetComponent<Parallax>().timeToStop = mediumParallax.GetComponent<Parallax>().startTimeToStop;

        for (int i = 0; i < virtualCamera.Length; i++)
        {
            if (virtualCamera[i].gameObject.activeSelf)
            {
                virtualCamera[i].gameObject.SetActive(false);
                if (i > 0)
                {
                    virtualCamera[i - 1].gameObject.SetActive(true);
                    cameraRef.GetComponent<CameraBehaviour>().actualCamera = virtualCamera[i - 1].gameObject.GetComponent<CinemachineVirtualCamera>();
                }
                break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        cameraRef = GameObject.Find("Main Camera");

        for (int i = 0; i < virtualCamera.Length; i++)
        {
            if (virtualCamera[i].gameObject.activeSelf)
            {
                cameraRef.GetComponent<CameraBehaviour>().actualCamera = virtualCamera[i].gameObject.gameObject.GetComponent<CinemachineVirtualCamera>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                GoNextCamera();
            }

            if (collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                GoPreviusCamera();
            }

            StartCoroutine(collision.GetComponentInParent<PlayerScr>().GoNextRoom());
        }
    }
}
