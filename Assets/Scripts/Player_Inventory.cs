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
        pants.Enqueue("Gray");
        shirts.Enqueue("Blue");
        suits.Enqueue("Robe");
    }
    public string CurrentItem(Queue<string> item) 
    {
        string p = "";
        if (pants.Count > 0)
        {
            return p = item.Peek();
        }
        else return null;
    }
    public void NextItem(Queue<string> item) 
    {
        item.Enqueue(item.Peek());
        item.Dequeue();
    }
    public void SellPants() 
    {
        if (pants.Count > 0)
        pants.Dequeue();
    }

    public void BuyPants() 
    {
        pants.Enqueue("Gray");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
