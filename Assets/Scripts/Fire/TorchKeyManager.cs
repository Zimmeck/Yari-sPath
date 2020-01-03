using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchKeyManager : MonoBehaviour
{
    public bool allTorchsInFire;
    private int childNum;
    public int childNumInFire;
    public GameObject door;

    //FUNCION PARA UNIR CON UN LINE RENDERER LAS ANTORCHAS QE SEAN KEY????

    public void IncreaseIntInFireTorchs()
    {
        childNumInFire += 1;
    }

    public void DecreaseIntInFireTorchs()
    {
        childNumInFire -= 1;
    }

    public void CheckTorchKeys()
    {
        childNum = transform.childCount;


        if (childNum == childNumInFire)
        {
            allTorchsInFire = true;

            //DO STUFF HERE
        }
        else
        {
            allTorchsInFire = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allTorchsInFire)
        {
            door.GetComponent<Door>().OpenDoor();
        }
        else
        {
            door.GetComponent<Door>().CloseDoor();
        }
    }
}
