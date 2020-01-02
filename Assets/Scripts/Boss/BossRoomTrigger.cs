using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BossRoomTrigger : MonoBehaviour
{
    public GameObject bossCam;
    public GameObject bossGate;


    public void ResetSelectedAbilities()
    {
        FindObjectOfType<GameManager>().bouncySelected = false;
        FindObjectOfType<GameManager>().bombSelected = false;
        FindObjectOfType<GameManager>().fireSelected = false;
        FindObjectOfType<GameManager>().staySelected = false;

        FindObjectOfType<SelectAbWithAngle>().ResetSelectedAb();
    }

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
            FindObjectOfType<Boss1>().activated = true;
            FindObjectOfType<Boss1>().timeToShoot = FindObjectOfType<Boss1>().startTimeToShoot;
            bossGate.GetComponent<Animator>().SetBool("closed", true);
            FindObjectOfType<CameraBehaviour>().actualCamera.gameObject.SetActive(false);
            bossCam.SetActive(true);
            FindObjectOfType<CameraBehaviour>().actualCamera = bossCam.GetComponent<CinemachineVirtualCamera>();
            FindObjectOfType<GameManager>().bouncy = 0;
            FindObjectOfType<GameManager>().bomb = 0;
            FindObjectOfType<GameManager>().fire = 0;
            FindObjectOfType<GameManager>().stay = 0;
            ResetSelectedAbilities();
            collision.GetComponentInParent<PlayerScr>().ResetAbilities();
            //Cosas
            Destroy(gameObject);
        }
    }
}
