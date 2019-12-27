using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform firePoint;
    public bool inFire;
    private GameObject fireInstance;
    public float timeToBurn;
    public float startTimeToBurn;

    public void GetFired()
    {
        if (!inFire)
        {
            inFire = true;
            fireInstance = Instantiate(firePrefab, firePoint.position, Quaternion.identity);
            fireInstance.transform.parent = firePoint.transform;
            timeToBurn = startTimeToBurn;
        }
    }

    public void UnGetFired()
    {
        if (inFire)
        {
            inFire = false;
            if (fireInstance != null) 
            {
                Destroy(fireInstance);
            }

            timeToBurn = startTimeToBurn;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (inFire)
        {
            fireInstance = Instantiate(firePrefab, firePoint.position, Quaternion.identity);
            fireInstance.transform.parent = firePoint.transform;
            timeToBurn = startTimeToBurn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inFire)
        {
            timeToBurn -= Time.deltaTime;
        }
        else
        {
            Destroy(fireInstance);
        }

        if (GetComponent<Torch>() == null && GetComponentInParent<SpearScr>() == null && GetComponentInParent<PreviewSpear>() == null)
        {
            if (inFire && timeToBurn <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ApagarFire>() != null)
        {
            if (inFire)
            {
                UnGetFired();
            }
        }   
    }
}
