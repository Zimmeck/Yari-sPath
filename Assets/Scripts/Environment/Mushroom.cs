using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject destroyParticles;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponentInParent<PlayerScr>().GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                Instantiate(destroyParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (collision.GetComponentInParent<SpearScr>() != null)
        {
            Instantiate(destroyParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
