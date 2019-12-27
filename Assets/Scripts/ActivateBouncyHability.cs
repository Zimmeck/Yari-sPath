using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBouncyHability : MonoBehaviour
{
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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerScr>().bouncySpearActive = 1;
            FindObjectOfType<GameManager>().bouncy = 1;
            PlayerPrefs.SetInt("bouncySpear", 1);
            Destroy(gameObject);
        }
    }
}
