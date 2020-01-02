using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePickableBloc : MonoBehaviour
{
    public GameObject brokeBlockParticle;

    public void DestroyObj()
    {
        Instantiate(brokeBlockParticle, transform.position, Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
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
        if (collision.GetComponent<TurretBullet>() != null)
        {

            DestroyObj();
            collision.GetComponent<DestroyOnPlatformCollision>().DestroyThing();
        }

        if (collision.GetComponent<Explossion>() != null)
        {
            DestroyObj();
        }

        if (collision.GetComponent<Fire>() != null)
        {
            if (GetComponentInParent<Flamable>() != null)
            {
                GetComponentInParent<Flamable>().GetFired();
            }
        }
    }

}
