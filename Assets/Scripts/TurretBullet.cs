using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explossion>() != null)
        {
            GetComponent<DestroyOnPlatformCollision>().DestroyThing();
        }

        if (collision.GetComponent<BouncyPlatform>() != null)
        {
            if (collision.GetComponent<BouncyPlatform>().isBullet == false)
            {
                //if (collision.GetComponent<BouncyPlatform>().isHorizontal == false)
                //{
                if (rb.velocity.x > 0 && rb.velocity.y == 0)
                {
                    transform.rotation = Quaternion.Euler(0,0,180);
                }

                if (rb.velocity.x < 0 && rb.velocity.y == 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                if (rb.velocity.y < 0 && rb.velocity.y <= 1f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }

                if (rb.velocity.y > 0 && rb.velocity.y >= -1f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                //}
            }
        }
    }
}
