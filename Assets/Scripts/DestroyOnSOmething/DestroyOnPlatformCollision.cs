using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlatformCollision : MonoBehaviour
{

    public GameObject destroyEffect;

    public void DestroyThing()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") || collision.GetComponentInParent<SpearScr>() != null || collision.GetComponentInParent<SteelBox>() != null)
        {
            if (GetComponent<Explosive>() != null)
            {
                GetComponent<Explosive>().Detonate();
            }

            DestroyThing();
        }
    }
}
