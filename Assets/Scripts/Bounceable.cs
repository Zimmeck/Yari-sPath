using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounceable : MonoBehaviour
{
    private Rigidbody2D rb;
    public void VerticalBounce(float bounceForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }

    public void HorizontalBounce(float bounceForce)
    {
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(bounceForce, 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
