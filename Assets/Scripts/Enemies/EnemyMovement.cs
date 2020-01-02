using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerScr>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x  != transform.position.x)
        {
            if (player.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            }
        }
    }

}
