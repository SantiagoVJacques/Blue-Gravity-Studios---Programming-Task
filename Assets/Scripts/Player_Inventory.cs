using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public int player_money = 100;
    public Queue<string> pants = new Queue<string>();
    public Queue<string> shirts = new Queue<string>();
    public Queue<string> suits = new Queue<string>();
    void Awake()
    {
        pants.Enqueue("Gray_Pants");
    }
    public string CurrentItem(Queue<string> item,string itemType) 
    {
        string p = "";
        if (pants.Count > 0&&itemType=="pants")
        {
            return p = item.Peek();
        }
        else if (shirts.Count > 0&&itemType=="shirts") 
        {
            return p = item.Peek();
        }
        else if (suits.Count > 0&&itemType=="suits") 
        {
            return p = item.Peek();
        }
        else return p;
    }

    public bool InventoryNotEmpty() 
    {
        if (suits.Count > 0) 
        {
            return true;
        }
        else if (pants.Count > 0) 
        {
            return true;
        }
        else if (shirts.Count > 0) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) 
        {
            Debug.Log(suits.Peek());
        }
    }
}
