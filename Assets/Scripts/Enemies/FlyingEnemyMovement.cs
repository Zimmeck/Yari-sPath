using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    private GameObject player;
    private float distanceToPlayer;
    private Rigidbody2D rb;
    public GameObject mySpr;
    public float minDistanceToChase;
    public float moveSpeed;
    public float moveSpeedV;
    public float maxSpeed;
    public GameObject deadParticles;
    public GameObject enemyToSpawn;
    public int enemyNumberToSpawn;
    private bool canBeHurt;
    private bool killed;
    private bool canMove;
    public float spawnSplashForce;
    // Start is called before the first frame update

    public void DieSpawning()
    {
        if (canBeHurt)
        {
            if (!killed)
            {
                if (enemyNumberToSpawn > 0)
                {
                    for (int i = 0; i < enemyNumberToSpawn; i++)
                    {
                        GameObject enemyInstance;
                        enemyInstance = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
                        enemyInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(10, -10), Random.Range(10, -10));
                    }
                }

                killed = true;
                Instantiate(deadParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DoInmuneTime()
    {
        canMove = false;
        yield return new WaitForSeconds(1);
        canBeHurt = true;
        canMove = true;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerScr>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        maxSpeed = Random.Range(5f, 20f);
        StartCoroutine(DoInmuneTime());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        //if (distanceToPlayer <= minDistanceToChase)
        //{
        //    if (player.transform.position.x > transform.position.x)
        //    {
        //        if (rb.velocity.x <= maxSpeed)
        //        {
        //            rb.velocity += new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
        //        }
        //    }
        //    else if (player.transform.position.x < transform.position.x)
        //    {
        //        if (rb.velocity.x >= -maxSpeed)
        //        {
        //            rb.velocity += new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);
        //        }
        //    }


        if (canMove)
        {
            if (distanceToPlayer <= minDistanceToChase)
            {
                if (player.transform.position.y != transform.position.y)
                {

                    if (player.transform.position.x > transform.position.x)
                    {
                        mySpr.transform.localScale = new Vector2(1, 1);

                        if (rb.velocity.x <= maxSpeed)
                        {
                            rb.velocity += new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
                        }
                    }
                    else if (player.transform.position.x < transform.position.x)
                    {
                        mySpr.transform.localScale = new Vector2(-1, 1);

                        if (rb.velocity.x >= -maxSpeed)
                        {
                            rb.velocity += new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);
                        }
                    }

                    if (player.transform.position.y < transform.position.y)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -moveSpeedV * Time.deltaTime);
                    }
                    else if (player.transform.position.y > transform.position.y)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, moveSpeedV * Time.deltaTime);
                    }
                }
            }
        
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
