using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    public float timeToDestroy;
    public float startTimeToRespawn;
    public float timeToRespawn;
    public GameObject brokeBlockParticle;
    public bool destroying;
    public bool destroyed;
    public GameObject destroyingParticlesObj;

    public void DestroyingTempBlock()
    {
        destroying = true;
        GetComponent<Animator>().SetBool("Destroying", true);
    }

    public void DisableObject()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        destroyingParticlesObj.SetActive(false);
        GetComponent<Animator>().SetBool("Destroying", false);
        timeToDestroy = 1;
        destroying = false;
        destroyed = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeToRespawn = startTimeToRespawn;
    }

    // Update is called once per frame
    void Update()
    {

        //Destroy temp block
        if (destroying)
        {
            timeToDestroy -= Time.deltaTime;
            destroyingParticlesObj.SetActive(true);
        }

        if (!destroyed)
        {
            if (timeToDestroy <= 0)
            {
                DisableObject();
            }

            if (!destroying)
            {
                destroyingParticlesObj.SetActive(false);
            }
        }


        if (destroyed)
        {
            timeToRespawn -= Time.deltaTime;
        }

        if (timeToRespawn <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            destroyingParticlesObj.SetActive(true);
            destroyed = false;
            timeToRespawn = startTimeToRespawn;
            GetComponent<Animator>().SetTrigger("Spawn");
            //Sonido "Bloop"
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponentInParent<PlayerScr>() != null)
        {
            DestroyingTempBlock();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<SpearScr>() != null || collision.GetComponentInParent<Explossion>() != null)
        {
            Instantiate(brokeBlockParticle, transform.position, Quaternion.identity);
            DisableObject();
        }
    }
}
