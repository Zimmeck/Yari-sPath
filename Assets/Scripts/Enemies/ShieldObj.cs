using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObj : MonoBehaviour
{
    public GameObject myStuckSpear;
    private Rigidbody2D myParentRb;
    private Vector3 scaleWhenStuck;
    // Start is called before the first frame update
    void Start()
    {
        myParentRb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleWhenStuck.x == -1)
        {
            if (myParentRb.velocity.x < 0)
            {
                if (myStuckSpear != null)
                {
                    myStuckSpear.transform.localScale = new Vector2(-1, 1);
                }
            }
            else if (myParentRb.velocity.x > 0)
            {
                if (myStuckSpear != null)
                {
                    myStuckSpear.transform.localScale = new Vector2(1, 1);
                }
            }
        }
        else if (scaleWhenStuck.x == 1)
        {
            if (myParentRb.velocity.x < 0)
            {
                if (myStuckSpear != null)
                {
                    myStuckSpear.transform.localScale = new Vector2(1, 1);
                }
            }
            else if (myParentRb.velocity.x > 0)
            {
                if (myStuckSpear != null)
                {
                    myStuckSpear.transform.localScale = new Vector2(-1, 1);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<SpearScr>() != null)
        {
            scaleWhenStuck = myParentRb.gameObject.transform.localScale;
            myStuckSpear = collision.transform.parent.gameObject;
        }
    }
}
