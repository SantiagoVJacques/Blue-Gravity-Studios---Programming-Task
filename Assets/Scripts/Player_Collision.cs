using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    public BoxCollider2D player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC" || collision.tag == "Door") 
        {
            Debug.Log("Abriste el menu");
        }
    }
}
