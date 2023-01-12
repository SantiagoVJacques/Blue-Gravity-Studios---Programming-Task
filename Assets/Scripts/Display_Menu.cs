using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Display_Menu : MonoBehaviour
{
    public Player_Inventory inventory;
    public GameObject npcMenu;
    public GameObject changeMenu;
    public PlayerMovement movement;
    public GameObject firstButtonShop;
    public GameObject shopInventory;
    public GameObject sellInventory;
    public GameObject firstItemShop;
    public bool shopOpen = false;
    public TMP_Text playerMoney;
    public Player_Collision player_Collision;
    //public GameObject firstButtonChange;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CloseMenu();
        if (player_Collision.canInteractNPC) 
        {
            if (Input.GetKeyDown("e")) 
            {
                OpenMenu("NPC");
            }
        }
        if (player_Collision.canInteractDoor) 
        {
            if (Input.GetKeyDown("e")) 
            {
                OpenMenu("Door");
            }
        }
    }

    public void OpenMenu(string check) 
    {
        if (check == "Door") 
        {
            changeMenu.SetActive(true);
        }
        if (check == "NPC") 
        {
            npcMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonShop);
        }
        movement.enabled = false;
    }

    public void CloseMenu() 
    {
        if (Input.GetKeyDown("escape")&&(shopOpen==false)) 
        {
            if (changeMenu.activeInHierarchy) 
            {
                changeMenu.SetActive(false);
                movement.enabled = true;
            }
            if (npcMenu.activeInHierarchy) 
            {
                npcMenu.SetActive(false);
                movement.enabled = true;
            }
        }
    }

    public void OpenShopInventory() 
    {
        playerMoney.text = inventory.player_money.ToString();
        shopOpen = true;
        npcMenu.SetActive(false);
        shopInventory.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstItemShop);
    }

    public void BuyItem(string itemType) 
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        string name = button.name;
        string val = button.transform.GetChild(3).GetComponent<TMP_Text>().text;
        int value = int.Parse(val);
        if (CheckPlayerInventory(itemType, name))
        {
            Debug.Log("Ya esta comprado");
        }
        else
        {
            if (inventory.player_money >= value)
            {
                if (itemType == "pants")
                {
                    inventory.pants.Enqueue(name);
                    inventory.player_money -= value;
                }
                else if (itemType == "shirts")
                {
                    inventory.shirts.Enqueue(name);
                    inventory.player_money -= value;
                }
                else
                {
                    inventory.suits.Enqueue(name);
                    inventory.player_money -= value;
                }
                playerMoney.text = inventory.player_money.ToString();
            }
            else
            {
                Debug.Log("no te alcanza");
            }
        }

    }

    public bool CheckPlayerInventory(string type,string item) 
    {
        if (type == "shirts") 
        {
            if (inventory.shirts.Contains(item)) return true;
            else return false;
        }
        else if (type == "suits")
        {
            if (inventory.suits.Contains(item)) return true;
            else return false;
        }
        else 
        {
            if (inventory.pants.Contains(item)) return true;
            else return false;
        }

    }

    public void Back2Shop() 
    {
        shopOpen = false;
        if (shopInventory.activeInHierarchy) 
        {
            shopInventory.SetActive(false);
            npcMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonShop);
        }
        if (sellInventory.activeInHierarchy)
        {
            sellInventory.SetActive(false);
            npcMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonShop);
        }

    }
}
