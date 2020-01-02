using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFireHability : MonoBehaviour
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
            collision.GetComponentInParent<PlayerScr>().fireSpearActive = 1;
            FindObjectOfType<GameManager>().fire = 1;
            PlayerPrefs.SetInt("fireSpear", 1);
            Destroy(gameObject);
        }
    }
}
