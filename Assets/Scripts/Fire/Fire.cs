using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireRange;
    public Vector2 squareSize;
    public LayerMask whatIsBomb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] thingsToFire = Physics2D.OverlapBoxAll(transform.position + new Vector3(0, .2f, 0), squareSize, 0);
        for (int i = 0; i < thingsToFire.Length; i++)
        {
            if (thingsToFire[i].GetComponentInParent<Explosive>() != null)
            {
                thingsToFire[i].GetComponentInParent<Explosive>().ExplodeBombTimerFloat();
            }

            if (thingsToFire[i].GetComponentInParent<Flamable>() != null)
            {
                thingsToFire[i].GetComponentInParent<Flamable>().GetFired();
            }

            if (thingsToFire[i].GetComponentInParent<SpearScr>() != null)
            {
                if (thingsToFire[i].GetComponentInParent<SpearScr>().isBombSpear)
                {
                    thingsToFire[i].GetComponentInParent<SpearScr>().ExplodeBombSpearInTimeFloat();
                }
            }
        }

        transform.eulerAngles = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Flamable>() != null)
        {
            collision.GetComponent<Flamable>().GetFired();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0, .4f, 0), squareSize);
    }

}
