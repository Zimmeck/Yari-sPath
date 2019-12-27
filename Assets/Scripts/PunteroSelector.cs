using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunteroSelector : MonoBehaviour
{
    public int abilityPickedCount;
    public int maxAbilities;
    public bool inButton;
    private GameObject actualButtonHovered;

    void SelectAbility()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (actualButtonHovered.CompareTag("SlotWeapon"))
            {


                if (!actualButtonHovered.GetComponent<AbilitySlot>().isSelected)
                {
                    if (abilityPickedCount < maxAbilities)
                    {
                        actualButtonHovered.GetComponent<AbilitySlot>().isSelected = true;
                        actualButtonHovered.GetComponent<AbilitySlot>().PickAbility();
                        abilityPickedCount += 1;
                    }
                }
                else
                {
                    actualButtonHovered.GetComponent<AbilitySlot>().isSelected = false;
                    actualButtonHovered.GetComponent<AbilitySlot>().UnPickAbility();
                    abilityPickedCount -= 1;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (inButton)
        {
            SelectAbility();
        }
    }

    //private void OnGUI()
    //{
    //    if (inButton)
    //    {
    //        SelectAbility();
    //    }
    //}




    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actualButtonHovered = collision.gameObject;
        inButton = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        actualButtonHovered = null;
        inButton = false;
    }



    //private void OnDisable()
    //{
    //    abilityPickedCount = 0;
    //}
}
