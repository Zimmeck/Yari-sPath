using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    private GameManager gm;
    public void DeleteProgress()
    {
        PlayerPrefs.DeleteAll();
        gm.collectibleCount = 0;
        gm.bouncy = 0;
        gm.bomb = 0;
        gm.fire = 0;
        gm.stay = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
