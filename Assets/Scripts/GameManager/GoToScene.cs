using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public string sceneName;

    public void GoToSceneByName()
    {
        GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().lastCheckPointPos = Vector2.zero;

        GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().collectibleCount = GameObject.Find("Player").GetComponent<PlayerScr>().collectibleCount;
        GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().lastCheckPointPos = Vector2.zero;
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                GoToSceneByName();
            }
        }
    }
}
