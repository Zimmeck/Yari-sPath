using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveEffect : MonoBehaviour
{
    [SerializeField]
    private Material material;

    private float dissolveAmount;
    private bool isDissolving = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - Time.deltaTime);
            material.SetFloat("_Disolveamount", dissolveAmount);
        }



        if (Input.GetKeyDown(KeyCode.Y))
        {
            isDissolving = false;
        }
    }
}
