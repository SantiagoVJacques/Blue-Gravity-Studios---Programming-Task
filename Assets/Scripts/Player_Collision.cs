using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    public BoxCollider2D player;
    public GameObject interactDoor;
    public GameObject interactNpc;
    public bool canInteractNPC = false;
    public bool canInteractDoor = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "NPC" )
        {
            canInteractNPC = true;
            interactNpc.SetActive(true);
        }
        if (collision.tag == "Door") 
        {
            canInteractDoor = true;
            interactDoor.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            canInteractNPC = false;
            interactNpc.SetActive(false);
        }
        if (collision.tag == "Door")
        {
            canInteractDoor = false;
            interactDoor.SetActive(false);
        }
    }

}
