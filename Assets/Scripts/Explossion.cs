using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explossion : MonoBehaviour
{
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
        if (collision.GetComponent<DestructibleWithBomb>() != null)
        {
            collision.GetComponent<DestructibleWithBomb>().DestroyBlock();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Flamable>() != null)
        {
            collision.GetComponent<Flamable>().GetFired();
        }
    }

}
