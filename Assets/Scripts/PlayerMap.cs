using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMap : MonoBehaviour
{
    public GameObject target;
    public bool travelling;
    public float speed;
    private GameManager gm;

    public void DeleteProgress()
    {
        PlayerPrefs.DeleteAll();
        gm.collectibleCount = 0;
        gm.ResetAbilities();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        target.transform.position =  GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().mapLastPosition;
        transform.position = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().mapLastPosition;
        if (!string.IsNullOrEmpty(gm.whatLevelCompleted))
        {
            string levelCompleted = gm.whatLevelCompleted;
            GameObject.Find(levelCompleted).GetComponent<Pin>().completed = 1;
            GameObject.Find(levelCompleted).GetComponent<Pin>().blocked = false;
            //gm.whatLevelCompleted = null;
        }

        PlayerPrefs.GetInt("collectibleCount");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().mapLastPosition = target.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.transform.position) > 0.2f)
        {
            travelling = true;
        }
        else
        {
            travelling = false;
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Pin>() != null)
        {
            if (!travelling)
            {
                if (Input.GetAxisRaw("Vertical") == 1)
                {
                    if (collision.GetComponent<Pin>().upPin != null && collision.GetComponent<Pin>().upPin.GetComponent<Pin>().blocked == false)
                    {
                        target.transform.position = collision.GetComponent<Pin>().upPin.transform.position;
                    }
                }

                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    if (collision.GetComponent<Pin>().rightPin != null && collision.GetComponent<Pin>().rightPin.GetComponent<Pin>().blocked == false)
                    {
                        target.transform.position = collision.GetComponent<Pin>().rightPin.transform.position;
                    }
                }

                if (Input.GetAxisRaw("Vertical") == -1)
                {
                    if (collision.GetComponent<Pin>().downPin != null && collision.GetComponent<Pin>().downPin.GetComponent<Pin>().blocked == false)
                    {
                        target.transform.position = collision.GetComponent<Pin>().downPin.transform.position;
                    }
                }

                if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    if (collision.GetComponent<Pin>().leftPin != null && collision.GetComponent<Pin>().leftPin.GetComponent<Pin>().blocked == false)
                    {
                        target.transform.position = collision.GetComponent<Pin>().leftPin.transform.position;
                    }
                }
            }
        }
    }
}
