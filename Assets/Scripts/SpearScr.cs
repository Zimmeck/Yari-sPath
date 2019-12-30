using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScr : MonoBehaviour
{
    private Rigidbody2D rb;
    private RaycastHit2D wallInfo;
    public float moveSpeed;
    public Collider2D myCollider;
    public float timeToExplode;
    public bool exploding;
    public float startSpearBombTimer;
    public float spearBombTimer;
    public float bounceForce;

    public Transform detectionPoint;
    public LayerMask platformLayer;
    public GameObject brokeSpearEffect;
    public GameObject stuckSpearEffect;

    public bool stuck;
    private bool stuckInMovingObject;
    public GameObject spearCopyForMovingObject;
    private Animator animator;
    public bool isLast;
    public SpriteRenderer mySprite;
    public GameObject explosionPrefab;
    public GameObject firePrefab;
    private bool thereWasStaySpear;


    //Types
    public bool isBouncySpear;
    public bool isBombSpear;
    public bool isFireSpear;
    public bool isStaySpear;


    private void DestroyLastSpear()
    {
        CheckStaySpear();

        if (!thereWasStaySpear)
        {
            if (transform.parent.transform.childCount == FindObjectOfType<PlayerScr>().maxSpears + 1)
            {
                transform.parent.transform.GetChild(0).GetComponent<SpearScr>().DestroySpear();
            }
        }
        thereWasStaySpear = false;
    }

    public void CheckStaySpear()
    {
        if (isStaySpear)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).GetComponent<SpearScr>().isStaySpear)
                {
                    if (transform.parent.GetChild(i).GetComponent<SpearScr>() != this)
                    {
                        thereWasStaySpear = true;
                        transform.parent.GetChild(i).GetComponent<SpearScr>().DestroySpear();
                    }
                }
            }
        }
    }


    public void DestroySpear()
    {   
        Instantiate(brokeSpearEffect, transform.position, Quaternion.identity);
        if (spearCopyForMovingObject != null)
        {
            Destroy(spearCopyForMovingObject.gameObject);
        }
        Destroy(gameObject);
    }

    public IEnumerator ExplodeBombSpearInTime()
    {
        if (!exploding)
        {
            exploding = true;
            yield return new WaitForSeconds(timeToExplode);
            GameObject explosionInstance = Instantiate(explosionPrefab, detectionPoint.position, Quaternion.identity);
            DestroySpear();
        }

    }

    public void ExplodeBombSpearInTimeFloat()
    {
        if (!exploding)
        {
            exploding = true;
            spearBombTimer = startSpearBombTimer;
        }
    }

    void Detonate()
    {
        GameObject explosionInstance = Instantiate(explosionPrefab, detectionPoint.position, Quaternion.identity);
        FindObjectOfType<VibrationManager>().Vibrate(.5f, .5f, .1f);
        DestroySpear();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        if (GetComponent<DarkSpear>() == null)
        {
            DestroyLastSpear();
        }


        if (isBouncySpear)
        {
            mySprite.color = Color.yellow;
        }

        if (isBombSpear)
        {
            mySprite.color = Color.red;
        }

        if (isFireSpear)
        {
           
            GetComponentInChildren<Flamable>().GetFired();

            if (isBombSpear)
            {
                ExplodeBombSpearInTimeFloat();
            }
        }

        if (isStaySpear)
        {
            mySprite.color = Color.grey;
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStaySpear)
        {
            transform.SetSiblingIndex(transform.parent.transform.childCount -1);
        }

        if (!stuck)
        {
            if (gameObject.CompareTag("SpearR"))
            {
                rb.velocity = transform.right * moveSpeed;
                //wallInfo = Physics2D.Raycast(detectionPoint.position, Vector2.right * transform.localScale, .2f, platformLayer);
            }

            if (gameObject.CompareTag("SpearL"))
            {
                rb.velocity = transform.right * -moveSpeed;
                //wallInfo = Physics2D.Raycast(detectionPoint.position, -Vector2.right * transform.localScale, .2f, platformLayer);
            }

            if (gameObject.CompareTag("SpearU"))
            {
                rb.velocity = transform.up * moveSpeed;
                //wallInfo = Physics2D.Raycast(detectionPoint.position, Vector2.up * transform.localScale, .2f, platformLayer);
            }

            //if (wallInfo)
            //{
            //    stuck = true;
            //    rb.velocity = Vector2.zero;
            //    GetComponentInChildren<BoxCollider2D>().isTrigger = false;
            //    rb.isKinematic = true;
            //}

        }
        else
        {
            if (GetComponent<DarkSpear>() == null)
            {
                if (isLast)
                {
                    animator.SetBool("isLast", true);
                }
                else
                {
                    animator.SetBool("isLast", false);
                }
            }

            if (stuckInMovingObject && spearCopyForMovingObject != null)
            {
                gameObject.transform.position = spearCopyForMovingObject.transform.position;
            }

            if (stuckInMovingObject && spearCopyForMovingObject == null)
            {

                DestroySpear();
            }
        }

        if (GetComponent<DarkSpear>() == null)
        {
            if (transform.parent.gameObject.transform.childCount == 3)
            {
                if (gameObject.transform.parent.GetChild(0).gameObject == gameObject && !isStaySpear)
                {
                    isLast = true;
                }
                else
                {
                    isLast = false;
                }
            }
            else
            {
                isLast = false;
            }
        }


        if (isBombSpear)
        {
            if (exploding)
            {
                spearBombTimer -= Time.deltaTime;
            }

            if (spearBombTimer < 0)
            {
                Detonate();
            }
        }
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingSpike"))
        {
            DestroySpear();
        }

        if (collision.CompareTag("Platform"))
        {

            //Colision con movingObject
            if (collision.GetComponentInParent<Door>() != null || collision.GetComponent<DestructibleWithBomb>() != null 
                || collision.GetComponentInParent<ShieldEnemy>() != null || collision.GetComponentInParent<SwitchLefPlatform>() != null 
                || collision.GetComponentInParent<SwitchRightPlatform>() != null && (GetComponent<DarkSpear>() == null))
            {
                spearCopyForMovingObject = new GameObject();
                spearCopyForMovingObject.transform.position = gameObject.transform.position;
                spearCopyForMovingObject.transform.parent = collision.transform;

                if(collision.GetComponentInParent<ShieldEnemy>() != null)
                {
                    spearCopyForMovingObject.transform.parent = collision.transform.parent.transform;
                }
                stuckInMovingObject = true;

                animator.SetTrigger("isStuck");
            }

            if (collision.GetComponent<DestructibleBlock>() == null && collision.GetComponent<NoDarkSpearCollision>() == null)
            {
                Instantiate(stuckSpearEffect, transform.Find("Sprite").transform.Find("Punta").transform.position, Quaternion.identity);
                stuck = true;
                rb.velocity = Vector2.zero;
                myCollider.isTrigger = false;
                rb.isKinematic = true;

                animator.SetTrigger("isStuck");
            }
        }

        if (collision.GetComponent<DestroySpearOnCollision>() != null && GetComponent<DarkSpear>() != null)
        {
            if (GetComponent<DarkSpear>() != null)
            {
                if (collision.gameObject.name == "BossMask")
                {
                    DestroySpear();
                }
            }

            Instantiate(stuckSpearEffect, transform.Find("Sprite").transform.Find("Punta").transform.position, Quaternion.identity);
            stuck = true;
            rb.velocity = Vector2.zero;
            myCollider.isTrigger = false;
            rb.isKinematic = true;

            animator.SetTrigger("isStuck");
        }

            //if (collision.GetComponentInParent<CompanyCube>() != null)
            //{
            //    collision.transform.parent = transform;
            //}

        if (!stuck)
        {
            if (collision.CompareTag("SpearL") || collision.CompareTag("SpearR") || collision.CompareTag("SpearU"))
            {
                //Destruir la lanza already clavada
                if (collision.GetComponentInParent<SpearScr>() != null)
                {
                    collision.GetComponentInParent<SpearScr>().DestroySpear();
                }

            }

            if (collision.GetComponent<FlyingEnemyMovement>() != null)
            {
                collision.GetComponent<FlyingEnemyMovement>().DieSpawning();
            }
        }
        else
        {
            if (isBombSpear)
            {
                ExplodeBombSpearInTimeFloat();
            }
        }

        //Destroyspear on explosion
        if (collision.GetComponent<Explossion>() != null)
        {
            DestroySpear();
        }

        if (collision.GetComponent<DarkSpear>() != null)
        {
            if (GetComponent<DarkSpear>() != null)
            {
                
                DestroySpear();
            }
        }

            if (GetComponent<DarkSpear>() == null)
        {
            if (collision.GetComponent<DestroySpearOnCollision>() != null)
            {
                if (!stuck)
                {
                    DestroySpear();
                }
            }
        }


        if (collision.GetComponent<Fire>() != null)
        {
            GetComponentInChildren<Flamable>().GetFired();
        }

        if (collision.GetComponent<ApagarFire>() != null)
        {
            GetComponentInChildren<Flamable>().UnGetFired();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBouncySpear)
        {
            if (gameObject.CompareTag("SpearR") || gameObject.CompareTag("SpearL"))
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    if (collision.gameObject.transform.position.y > transform.position.y)
                    {
                        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
                        {
                            collision.gameObject.GetComponent<PlayerScr>().Jump(collision.gameObject.GetComponent<PlayerScr>().bouncySpearJumpSpeed);
                        }       
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

            if (gameObject.CompareTag("SpearU"))
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
}

