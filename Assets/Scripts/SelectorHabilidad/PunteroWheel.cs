using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunteroWheel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * -180 / Mathf.PI);
        }

        //Vector3 lookVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 4096);

        //Quaternion targetRotation = Quaternion.LookRotation(lookVec, Vector3.back);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
