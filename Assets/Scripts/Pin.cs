using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pin : MonoBehaviour
{
    private GameObject player;
    private GameObject target;
    public bool blocked;
    public int completed;
    public int whatUnblockThis;

    public GameObject upPin;
    public GameObject rightPin;
    public GameObject downPin;
    public GameObject leftPin;

    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.Find("Target");


        completed = PlayerPrefs.GetInt(sceneName);


    }

    // Update is called once per frame
    void Update()
    {
        if (blocked)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }

        if (gameObject.CompareTag("Corner"))
        {
            var tempColor = GetComponent<SpriteRenderer>().color;
            tempColor.a = 0.2f;
            GetComponent<SpriteRenderer>().color = tempColor;

        }


        //if (completed == 1)
        //{
        //    if (upPin != null)
        //    {
        //        upPin.GetComponent<Pin>().blocked = false;
        //    }

        //    if (rightPin != null)
        //    {
        //        rightPin.GetComponent<Pin>().blocked = false;
        //    }

        //    if (downPin != null)
        //    {
        //        downPin.GetComponent<Pin>().blocked = false;
        //    }

        //    if (leftPin != null)
        //    {
        //        leftPin.GetComponent<Pin>().blocked = false;
        //    }


        //}

        //ir por un lao bloqea el otro

        //DUDA DISEÑO:  Que queremos esto o lo otro
        //esto sería raro y lo otro sería si nunca se va a poder ir por el camino 2 antes que por el 1

        //if (upPin != null)
        //{
        //    if (upPin.GetComponent<Pin>().completed == whatUnblockThis)
        //    {
        //        blocked = false;
        //    }
        //}

        //if (rightPin != null)
        //{
        //    if (rightPin.GetComponent<Pin>().completed == whatUnblockThis)
        //    {
        //        blocked = false;
        //    }
        //}

        //if (downPin != null)
        //{
        //    if (downPin.GetComponent<Pin>().completed == whatUnblockThis)
        //    {
        //        blocked = false;
        //    }
        //}

        //if (leftPin != null)
        //{
        //    if (leftPin.GetComponent<Pin>().completed == whatUnblockThis)
        //    {
        //        blocked = false;
        //    }
        //}

        //Ir por el lao 2 desbloquea los 2

        if (blocked)
        {
            if (upPin != null)
            {
                if (upPin.GetComponent<Pin>().completed >= whatUnblockThis)
                {
                    blocked = false;
                }
            }

            if (rightPin != null)
            {
                if (rightPin.GetComponent<Pin>().completed >= whatUnblockThis)
                {
                    blocked = false;
                }
            }

            if (downPin != null)
            {
                if (downPin.GetComponent<Pin>().completed >= whatUnblockThis)
                {
                    blocked = false;
                }
            }

            if (leftPin != null)
            {
                if (leftPin.GetComponent<Pin>().completed >= whatUnblockThis)
                {
                    blocked = false;
                }
            }
        }


        if (gameObject.CompareTag("Corner"))
        {
            if (!blocked)
            {
                if (upPin != null)
                {
                    upPin.GetComponent<Pin>().blocked = false;
                }

                if (rightPin != null)
                {
                    rightPin.GetComponent<Pin>().blocked = false;
                }

                if (downPin != null)
                {
                    downPin.GetComponent<Pin>().blocked = false;
                }

                if (leftPin != null)
                {
                    leftPin.GetComponent<Pin>().blocked = false;
                }
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (upPin != null)
        {
            Gizmos.DrawLine(transform.position, upPin.transform.position);
        }

        if (rightPin != null)
        {
            Gizmos.DrawLine(transform.position, rightPin.transform.position);
        }

        if (downPin != null)
        {
            Gizmos.DrawLine(transform.position, downPin.transform.position);
        }

        if (leftPin != null)
        {
            Gizmos.DrawLine(transform.position, leftPin.transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //go to level
            if (!string.IsNullOrEmpty(sceneName))
            {
                if (Input.GetButtonDown("Jump"))
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}
