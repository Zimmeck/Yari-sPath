using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmEnemy : MonoBehaviour
{
    public GameObject spearParent;
    public bool speared;
    private Animator myAnim;
    public GameObject hurtEffect;
    public GameObject bleedEffectObj;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!speared)
        {
            GetComponent<Turret>().enabled = true;
            var em = bleedEffectObj.GetComponent<ParticleSystem>().emission;
            em.enabled = false;
            myAnim.SetBool("Speared", false);

        }
        else
        {
            GetComponent<Turret>().enabled = false;
            GetComponent<Turret>().timeToShot1 = GetComponent<Turret>().startTimeToShot1;
            var em = bleedEffectObj.GetComponent<ParticleSystem>().emission;
            em.enabled = true;
            myAnim.SetBool("Speared", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("SpearL") || collision.transform.parent.gameObject.CompareTag("SpearR"))
        {
            if (collision.transform.parent.gameObject.GetComponent<SpearScr>().stuck = true)
            {
                if (!speared)
                {
                    speared = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("SpearL") || collision.transform.parent.gameObject.CompareTag("SpearR"))
        {
            if (speared)
            {
                speared = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("SpearL") || collision.transform.parent.gameObject.CompareTag("SpearR"))
        {
            if(collision.GetComponentInParent<SpearScr>().stuck == true)
            {
                Instantiate(hurtEffect, transform.position, Quaternion.identity);
            } 
        }
    }

}
