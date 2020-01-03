using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public bool isKey;
    public bool inFire;


    public void ActivateTorchKey()
    {
        if (!inFire)
        {
            inFire = true;
            transform.parent.gameObject.GetComponent<TorchKeyManager>().IncreaseIntInFireTorchs();
            transform.parent.gameObject.GetComponent<TorchKeyManager>().CheckTorchKeys();
        }
    }

    public void DeactivateTorchKey()
    {
        if (inFire)
        {
            inFire = false;
            transform.parent.gameObject.GetComponent<TorchKeyManager>().DecreaseIntInFireTorchs();
            transform.parent.gameObject.GetComponent<TorchKeyManager>().CheckTorchKeys();
        }

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
