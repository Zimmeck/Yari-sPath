using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLefPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().enabled == false)
        {
            if (transform.childCount != 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }
}
