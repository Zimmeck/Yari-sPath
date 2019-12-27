using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossWeakPoint : MonoBehaviour
{
    public bool withMask;
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
        if (collision.GetComponentInParent<SpearScr>() != null)
        {
            if (collision.GetComponentInParent<DarkSpear>() == null)
            {
                    collision.GetComponentInParent<SpearScr>().DestroySpear();
                    GetComponentInParent<Boss1>().DecreaseHealth();
            }
        }
    }
}
