using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Display_Menu : MonoBehaviour
{
    public Player_Inventory inventory;
    public PlayerMovement movement;
    public Player_Collision player_Collision;
    public GameObject playerModel;
    public GameObject player;
    public GameObject npcMenu;
    public GameObject changeMenu;
    public GameObject firstButtonShop;
    public GameObject shopInventory;
    public GameObject sellInventory;
    public GameObject firstItemShop;
    public GameObject firstItemInventory=null;
    public GameObject firstItemChange=null;
    public GameObject sellStartingPoint;
    public GameObject buttonTemplate;
    public GameObject changeStartingPoint;
    List<GameObject> sellButtons = new List<GameObject>();
    List<GameObject> changeButtons = new List<GameObject>();
    public TMP_Text playerMoney;
    public TMP_Text playerMoney2;
    public TMP_Text NPCText;
    public TMP_Text NPCText2;
    public TMP_Text WorldText;
    public Sprite Green_Shirt;
    public Sprite Blue_Shirt;
    public Sprite White_Shirt;
    public Sprite Gray_Pants;
    public Sprite Green_Pants;
    public Sprite Yellow_Pants;
    public Sprite Armor;
    public Sprite Robe;
    public bool destoyText=false;
    public bool shopOpen = false;
    float time = 0;

    //public GameObject firstButtonChange;
    void Start()
    {
        WorldText.text = "";
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
        if (destoyText) 
        {
            
            time+=Time.deltaTime;
            if (time >= 3) 
            {
                destoyText = false;
                NPCText.text = "";
                WorldText.text = "";
                time = 0;
            }
        }
    }

    public void OpenMenu(string check) 
    {
        if (check == "Door") 
        {
            ChangeMenu();
            return;
        }
        if (check == "NPC") 
        {
            npcMenu.SetActive(true);
            NPCText2.text= "Welcome to my little shop. \nWhat would you like to do ? ";
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
                for (int i = 0; i < changeButtons.Count; i++)
                {
                    Destroy(changeButtons[i].gameObject);
                }
                changeButtons.Clear();
            }
            if (npcMenu.activeInHierarchy) 
            {
                npcMenu.SetActive(false);
                movement.enabled = true;
            }
        }
    }

    public void CloseButton() 
    {
        if (changeMenu.activeInHierarchy)
        {
            changeMenu.SetActive(false);
            movement.enabled = true;
            for (int i = 0; i < changeButtons.Count; i++)
            {
                Destroy(changeButtons[i].gameObject);
            }
            changeButtons.Clear();
        }
    }

    public void CloseShopMenu() 
    {
        if (npcMenu.activeInHierarchy) 
        {
            npcMenu.SetActive(false);
            movement.enabled = true;
        }
    }

    public void OpenShopInventory() 
    {
        NPCText.text = "";
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
            NPCText.text = "You already have one in your inventory";
            destoyText = true;
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
                NPCText.text = "Thanks for buying here";
                destoyText = true;
            }
            else
            {
                NPCText.text = "You do not have enough money";
                destoyText = true;
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
        NPCText2.text = "Welcome to my little shop. \nWhat would you like to do ? ";
        if (shopInventory.activeInHierarchy) 
        {
            shopInventory.SetActive(false);
            npcMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonShop);
        }
        if (sellInventory.activeInHierarchy)
        {
            if (sellButtons.Count > 0)
            {
                UpdateSellInventory();
            }
            sellInventory.SetActive(false);
            npcMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonShop);
        }

    }

    public void OpenSellInventory() 
    {
        if (inventory.InventoryNotEmpty()) 
        {
            playerMoney2.text = inventory.player_money.ToString();
            shopOpen = true;
            npcMenu.SetActive(false);
            CreateInventory(buttonTemplate, sellStartingPoint,true);
            sellInventory.SetActive(true); 
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstItemInventory);
        }
        else 
        {
            NPCText2.text = "You have nothing to sell";
        }
    }

    public void CreateInventory(GameObject button,GameObject parent,bool sell) 
    {
        List<string> temp = new List<string>();
        
        if (inventory.suits.Count > 0) 
        {     
            for(int i = 0; i < inventory.suits.Count; i++) 
            {
                temp.Add(inventory.suits.Peek());
                inventory.suits.Enqueue(inventory.suits.Peek());
                inventory.suits.Dequeue();
            }
        }
        if (inventory.shirts.Count > 0) 
        {
            for (int i = 0; i < inventory.shirts.Count; i++)
            {
                temp.Add(inventory.shirts.Peek());
                inventory.shirts.Enqueue(inventory.shirts.Peek());
                inventory.shirts.Dequeue();
            }
        }
        if (inventory.pants.Count > 0) 
        {
            for (int i = 0; i < inventory.pants.Count; i++)
            {
                temp.Add(inventory.pants.Peek());
                inventory.pants.Enqueue(inventory.pants.Peek());
                inventory.pants.Dequeue();
            }
        }
        for(int i = 0; i < temp.Count; i++) 
        {

            GameObject tempButt = new GameObject();
            tempButt=Instantiate(button, parent.transform);
            tempButt.name = temp[i];
            tempButt.transform.GetChild(0).GetComponent<TMP_Text>().text = temp[i];
            if (temp[i]=="Armor")
            {
                tempButt.transform.GetChild(2).GetComponent<Image>().sprite = Armor;
            }
            if (temp[i]=="Robe")
            {
                tempButt.transform.GetChild(2).GetComponent<Image>().sprite = Robe;
            }
            if (temp[i] == "White_Shirt")
            {
                tempButt.transform.GetChild(2).GetComponent<Image>().sprite = White_Shirt;
            }
            if (temp[i] == "Blue_Shirt")
            {
                tempButt.transform.GetChild(2).GetComponent<Image>().sprite = Blue_Shirt;
            }
            if (temp[i] == "Green_Shirt")
            {
                tempButt.transform.GetChild(2).GetComponent<Image>().sprite = Green_Shirt;
            }
            if (temp[i] == "Gray_Pants")
            {
                tempButt.transform.GetChild(2).GetComponent<Image>().sprite = Gray_Pants;
            }
            if (temp[i] == "Green_Pants")
            {
                tempButt.transform.GetChild(2).GetComponent<Image>().sprite = Green_Pants;
            }
            if (temp[i] == "Yellow_Pants")
            {
                tempButt.transform.GetChild(2).GetComponent<Image>().sprite = Yellow_Pants;
            }
            if (sell) 
            {
                tempButt.GetComponent<Button>().onClick.AddListener(sellItem);
                sellButtons.Add(tempButt); 
            }
            else 
            {
                tempButt.GetComponent<Button>().onClick.AddListener(SelectOutfit);
                changeButtons.Add(tempButt);
            }
            tempButt.GetComponent<RectTransform>().position = new Vector3(tempButt.GetComponent<RectTransform>().position.x, 977-(100*i), 0);
        }

        EventSystem.current.SetSelectedGameObject(null);
        if (sellButtons.Count > 0) 
        {
            firstItemInventory = sellButtons[0].gameObject;
            EventSystem.current.SetSelectedGameObject(firstItemInventory);
        }
        else 
        {
            EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Back"));
        }

    }

    public void UpdateSellInventory() 
    {
        for (int i = 0; i < sellButtons.Count; i++)
        {
            Destroy(sellButtons[i].gameObject);
        }
        sellButtons.Clear();
    }
    public void sellItem() 
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        if (inventory.suits.Count > 0) 
        {
            for(int i = 0; i < inventory.suits.Count; i++) 
            {
                if (button.name == inventory.suits.Peek()) 
                {
                    inventory.suits.Dequeue();
                    inventory.player_money += int.Parse(button.transform.GetChild(3).GetComponent<TMP_Text>().text);
                    playerMoney2.text = inventory.player_money.ToString();
                    UpdateSellInventory();
                    CreateInventory(buttonTemplate,sellStartingPoint,true);
                    if (!inventory.InventoryNotEmpty())
                    {
                        sellInventory.SetActive(false);
                        npcMenu.SetActive(true);
                        shopOpen = false;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(firstButtonShop);
                        NPCText2.text = "You have no items. \n Do you want to buy something?";
                    }

                }
                else 
                {
                    inventory.suits.Enqueue(inventory.suits.Peek());
                    inventory.suits.Dequeue();
                }
            }
            
        }
        if (inventory.pants.Count > 0)
        {
            for (int i = 0; i < inventory.pants.Count; i++)
            {
                if (button.name == inventory.pants.Peek())
                {
                    inventory.pants.Dequeue();
                    inventory.player_money += int.Parse(button.transform.GetChild(3).GetComponent<TMP_Text>().text);
                    playerMoney2.text = inventory.player_money.ToString();
                    UpdateSellInventory();
                    CreateInventory(buttonTemplate, sellStartingPoint,true);
                    if (!inventory.InventoryNotEmpty())
                    {
                        sellInventory.SetActive(false);
                        npcMenu.SetActive(true);
                        shopOpen = false;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(firstButtonShop);
                        NPCText2.text = "You have no items. \n Do you want to buy something?";
                    }
                    return;
                }
                else
                {
                    inventory.pants.Enqueue(inventory.pants.Peek());
                    inventory.pants.Dequeue();
                }
            }
        }
        if (inventory.shirts.Count > 0)
        {
            for (int i = 0; i < inventory.shirts.Count; i++)
            {
                if (button.name == inventory.shirts.Peek())
                {
                    inventory.shirts.Dequeue();
                    inventory.player_money += int.Parse(button.transform.GetChild(3).GetComponent<TMP_Text>().text);
                    playerMoney2.text = inventory.player_money.ToString();
                    UpdateSellInventory();
                    CreateInventory(buttonTemplate, sellStartingPoint,true);
                    if (!inventory.InventoryNotEmpty())
                    {
                        sellInventory.SetActive(false);
                        npcMenu.SetActive(true);
                        shopOpen = false;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(firstButtonShop);
                        NPCText2.text = "You have no items. \n Do you want to buy something?";
                    }
                    return;
                }
                else
                {
                    inventory.shirts.Enqueue(inventory.shirts.Peek());
                    inventory.shirts.Dequeue();
                }
            }
        }
        
    }

    public void ChangeMenu() 
    {
        if (inventory.InventoryNotEmpty()) 
        {
            movement.enabled = false;
            changeMenu.SetActive(true);
            CreateInventory(buttonTemplate,changeStartingPoint,false);
        }
        else 
        {
            WorldText.text = "You have nothing to try on";
            destoyText = true;
            movement.enabled = true;
        }
    }

    public void SelectOutfit() 
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        if (inventory.suits.Count > 0) 
        {
            if (inventory.suits.Contains(button.name)) 
            {
                playerModel.transform.GetChild(0).gameObject.SetActive(false);
                playerModel.transform.GetChild(1).gameObject.SetActive(false);
                playerModel.transform.GetChild(2).gameObject.SetActive(true);
                movement.suitOff = true;
                for (int i = 0; i < inventory.suits.Count; i++) 
                {
                    if (inventory.suits.Peek() == button.name) 
                    {
                        playerModel.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = button.transform.GetChild(2).GetComponent<Image>().sprite;
                        return;
                    }
                    else 
                    {
                        inventory.suits.Enqueue(inventory.suits.Peek());
                        inventory.suits.Dequeue();
                    }
                }
            }
        }
        if (inventory.pants.Count > 0) 
        {
            if (inventory.pants.Contains(button.name)) 
            {
                playerModel.transform.GetChild(0).gameObject.SetActive(true);
                playerModel.transform.GetChild(1).gameObject.SetActive(true);
                playerModel.transform.GetChild(2).gameObject.SetActive(false);
                player.transform.GetChild(2).gameObject.SetActive(false);
                movement.suitOff = false;
                for(int i = 0; i < inventory.pants.Count; i++) 
                {
                    if (inventory.pants.Peek() == button.name) 
                    {
                        playerModel.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = button.transform.GetChild(2).GetComponent<Image>().sprite;
                        return;
                    }
                    else 
                    {
                        inventory.pants.Enqueue(inventory.pants.Peek());
                        inventory.pants.Dequeue();
                    }
                }
            }
        }
        if (inventory.shirts.Count > 0) 
        {
            if (inventory.shirts.Contains(button.name)) 
            {
                playerModel.transform.GetChild(0).gameObject.SetActive(true);
                playerModel.transform.GetChild(1).gameObject.SetActive(true);
                playerModel.transform.GetChild(2).gameObject.SetActive(false);
                player.transform.GetChild(2).gameObject.SetActive(false);
                movement.suitOff = false;
                for (int i = 0; i < inventory.shirts.Count; i++) 
                {
                    if (inventory.shirts.Peek() == button.name) 
                    {
                        playerModel.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = button.transform.GetChild(2).GetComponent<Image>().sprite;
                        return;
                    }
                    else 
                    {
                        inventory.shirts.Enqueue(inventory.shirts.Peek());
                        inventory.shirts.Dequeue();
                    }
                }
            }
        }
    }
}
