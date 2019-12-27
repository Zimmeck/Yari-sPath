using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWithBomb : MonoBehaviour
{
    public GameObject destroyBlockParticles;

    public void DestroyBlock()
    {
        Instantiate(destroyBlockParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
