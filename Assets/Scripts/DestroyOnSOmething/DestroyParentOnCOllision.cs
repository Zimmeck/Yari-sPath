using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentOnCOllision : MonoBehaviour
{
    public GameObject destroyEffect;

    public void DestroyTing()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
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
        if (collision.CompareTag("Platform"))
        {
            if (GetComponentInParent<SpearScr>() != null)
            {
                GetComponentInParent<SpearScr>().DestroySpear();
            }

            DestroyTing();
        }
    }
}
