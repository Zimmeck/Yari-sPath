using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float speedVaritaion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = Random.Range(speed - speedVaritaion, speed + speedVaritaion);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
    }
}
