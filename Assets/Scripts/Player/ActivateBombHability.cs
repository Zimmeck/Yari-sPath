using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBombHability : MonoBehaviour
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
            collision.GetComponentInParent<PlayerScr>().bombSpearActive = 1;
            FindObjectOfType<GameManager>().bomb = 1;
            PlayerPrefs.SetInt("bombSpear", 1);
            Destroy(gameObject);
        }
    }
}
