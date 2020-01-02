using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DienteLeon : MonoBehaviour
{
    public GameObject destroyParticles;
    public bool destroyed;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ||collision.GetComponentInParent<SpearScr>() != null)
        {
            if (!destroyed)
            {
                GameObject effectInstance = Instantiate(destroyParticles, transform.position, Quaternion.identity);

                if (collision.transform.parent.transform.position.x > transform.position.x)
                {
                    effectInstance.transform.localScale = new Vector2(-effectInstance.transform.localScale.x, effectInstance.transform.localScale.y);
                }
                destroyed = true;
                myAnimator.SetTrigger("Destroy");
                
            }
        }
    }
}
