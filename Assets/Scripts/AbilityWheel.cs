using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWheel : MonoBehaviour
{
    public GameObject wheelPanel;
    public bool wheelPanelActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JoysticRTap"))
        {
            if (wheelPanelActive)
            {
                wheelPanel.SetActive(false);
                wheelPanelActive = false;
            }
            else
            {
                wheelPanel.SetActive(true);
                wheelPanelActive = true;
            }

        }
    }
}
