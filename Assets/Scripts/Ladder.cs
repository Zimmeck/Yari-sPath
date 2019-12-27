using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    //Solo puede haber una onewayplatform colisionando con la escalera al mismo tiempo
    //Poner dos escaleras en caso de querer una escalera que atraviese mas de una onewayplatform
    //El colider tiene que estar un poco por arriba de la onewayplatform 

    public GameObject collidingPlatform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Platform"))
        {
            if (collision.GetComponent<PlatformEffector2D>() != null)
            {
                collidingPlatform = collision.gameObject;
            }

        }

        if (collision.GetComponentInParent<PlayerScr>() != null)
        {
            if (collision.GetComponentInParent<PlayerScr>().isClimbing)
            {
                if (collidingPlatform != null)
                {
                    if (collidingPlatform.transform.position.y < collision.transform.position.y)
                    {
                        collidingPlatform.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
                    }
                    else
                    {
                        collidingPlatform.GetComponent<PlatformEffector2D>().rotationalOffset = 0;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<PlayerScr>() != null)
        {
            if (collidingPlatform != null)
            {
                collidingPlatform.GetComponent<PlatformEffector2D>().rotationalOffset = 0;
            }
        }
    }
}
