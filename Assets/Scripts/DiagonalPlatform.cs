using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalPlatform : MonoBehaviour
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
        if (collision.gameObject.GetComponentInParent<SpearScr>() != null)
        {
            collision.gameObject.GetComponentInParent<SpearScr>().DestroySpear();
        }
    }
}
