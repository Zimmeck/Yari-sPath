using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public bool isPicked;
    public Collider2D myCollider;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            transform.position = transform.parent.transform.position;
            myCollider.isTrigger = true;
            rb.isKinematic = true;
        }
        else
        {
            myCollider.isTrigger = false;
            rb.isKinematic = false;
        }
    }
}
