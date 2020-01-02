using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool movingRight;
    public bool movingLeft;
    public float speed;

    public float timeToStop;
    public float startTimeToStop;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        timeToStop -= Time.deltaTime;

        if (timeToStop > 0)
        {
            if (movingRight && !movingLeft)
            {
                rb.velocity = transform.right * speed * Time.deltaTime;
            }

            if (movingLeft && !movingRight)
            {
                rb.velocity = -transform.right * speed * Time.deltaTime;
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
            movingRight = false;
            movingLeft = false;
        }


    }
}
