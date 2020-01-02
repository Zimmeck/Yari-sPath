using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    public bool isHorizontal;
    public bool isBullet;
    public float bounceForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (isHorizontal)
        {
            if (GetComponent<PlatformEffector2D>() != null)
            {
                GetComponent<PlatformEffector2D>().enabled = true;
                GetComponent<Collider2D>().usedByEffector = true;
            }
        }
        else
        {

            if (GetComponent<PlatformEffector2D>() != null)
            {
                GetComponent<PlatformEffector2D>().enabled = false;
                GetComponent<Collider2D>().usedByEffector = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

            if (isHorizontal)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    if (collision.gameObject.transform.position.y > transform.position.y)
                    {
                        //if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
                        //{
                            collision.gameObject.GetComponent<PlayerScr>().Jump(collision.gameObject.GetComponent<PlayerScr>().bouncySpearJumpSpeed);

                            if (gameObject.GetComponentInParent<TurretBullet>() != null)
                            {
                                gameObject.GetComponentInParent<DestroyOnPlatformCollision>().DestroyThing();
                            }
                        //}
                    }
                }

                if (collision.gameObject.GetComponent<Bounceable>() != null)
                {
                    if (collision.gameObject.transform.position.y > transform.position.y)
                    {
                        collision.gameObject.GetComponent<Bounceable>().VerticalBounce(10);
                    }
                }
            }

            if (!isHorizontal)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    if (collision.gameObject.transform.position.x > transform.position.x)
                    {
                        //StartCoroutine (collision.gameObject.GetComponent<PlayerScr>().HorizontalBounce(bounceForce));
                        collision.gameObject.GetComponent<PlayerScr>().HorizontalBouncy(bounceForce);
                    }
                    else if (collision.gameObject.transform.position.x < transform.position.x)
                    {
                        //StartCoroutine( collision.gameObject.GetComponent<PlayerScr>().HorizontalBounce(-bounceForce));
                        collision.gameObject.GetComponent<PlayerScr>().HorizontalBouncy(-bounceForce);
                    }
                }

                if (collision.gameObject.GetComponent<Bounceable>() != null)
                {

                    if (collision.gameObject.transform.position.x > transform.position.x)
                    {
                        collision.gameObject.GetComponent<Bounceable>().HorizontalBounce(10);
                    }
                    else if (collision.gameObject.transform.position.x < transform.position.x)
                    {
                        collision.gameObject.GetComponent<Bounceable>().HorizontalBounce(-10);
                    }
                }
            }
    }
}
