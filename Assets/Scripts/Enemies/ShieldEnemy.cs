using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool speared;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("asfsdag");
        if (collision.gameObject.GetComponentInParent<SpearScr>() != null)
        {
            Debug.Log("asfsdag");
            rb.isKinematic = true;
            //rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
