using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed=5;
    public Rigidbody2D playerRB;
    Vector2 movement;
    public Animator PlayerAnimator;
    public Animator LegsAnimator;
    public Animator ShirtAnimator;
    public Animator SuitAnimator;
    public bool isMoving = false;
    public Player_Inventory inventory;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (inventory.pants.Count == 0) 
        {
            LegsAnimator.gameObject.SetActive(false);
        }
        else if(inventory.pants.Count>0) 
        {
            LegsAnimator.gameObject.SetActive(true);
        }
        if (inventory.shirts.Count == 0) 
        {
            ShirtAnimator.gameObject.SetActive(false);
        }
        else if (inventory.shirts.Count > 0) 
        {
            ShirtAnimator.gameObject.SetActive(true);
        }
        if (inventory.suits.Count == 0)
        {
            SuitAnimator.gameObject.SetActive(false);
        }
        else if (inventory.suits.Count > 0)
        {
            SuitAnimator.gameObject.SetActive(true);
        }
        if (!isMoving) 
        {
            PlayerAnimator.Play("Body_Idle");
            if (inventory.pants.Count > 0)
            {
                LegsAnimator.Play(inventory.CurrentItem(inventory.pants,"pants") + "_Idle");
            }

            if (inventory.shirts.Count > 0)
            {
                ShirtAnimator.Play(inventory.CurrentItem(inventory.shirts,"shirts") + "_Idle");
            }

            if (inventory.suits.Count > 0)
            SuitAnimator.Play(inventory.CurrentItem(inventory.suits,"suits") + "_Idle");
            
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x != 0) movement.y = 0;
        if (movement.y != 0) movement.x = 0;
        if (movement.sqrMagnitude != 0) 
        {
            isMoving = true;
        }
        if (movement.sqrMagnitude == 0) 
        {
            isMoving = false;
        }
        if (movement.x < 0) 
        {
            PlayerAnimator.Play("Body_Walking_Left");
            if (inventory.pants.Count > 0)
            LegsAnimator.Play(inventory.CurrentItem(inventory.pants, "pants") +"_Walking_Left");
            
            if (inventory.shirts.Count > 0)
            ShirtAnimator.Play(inventory.CurrentItem(inventory.shirts, "shirts") + "_Left");
            
            if (inventory.suits.Count > 0)
            SuitAnimator.Play(inventory.CurrentItem(inventory.suits, "suits") + "_Left");
        }
        if (movement.x > 0)
        {
            PlayerAnimator.Play("Body_Walking_Right");
            if (inventory.pants.Count > 0)
            LegsAnimator.Play(inventory.CurrentItem(inventory.pants, "pants") + "_Walking_Right");
            
            if (inventory.shirts.Count > 0)
            ShirtAnimator.Play(inventory.CurrentItem(inventory.shirts, "shirts") + "_Right");
            if (inventory.suits.Count > 0)
            SuitAnimator.Play(inventory.CurrentItem(inventory.suits, "suits") + "_Right");
        }
        if (movement.y < 0)
        {
            PlayerAnimator.Play("Body_Walking_Down");
            if (inventory.pants.Count > 0)
            LegsAnimator.Play(inventory.CurrentItem(inventory.pants, "pants") + "_Walking_Down");

            if (inventory.shirts.Count > 0)
            ShirtAnimator.Play(inventory.CurrentItem(inventory.shirts, "shirts") + "_Down");

            if (inventory.suits.Count > 0)
            SuitAnimator.Play(inventory.CurrentItem(inventory.suits, "suits") + "_Down");
        }
        if (movement.y > 0)
        {
            PlayerAnimator.Play("Body_Walking_Up");
            if (inventory.pants.Count > 0)
            LegsAnimator.Play(inventory.CurrentItem(inventory.pants, "pants") + "_Walking_Up");
            
            if (inventory.shirts.Count > 0)
            ShirtAnimator.Play(inventory.CurrentItem(inventory.shirts, "shirts") + "_Up");

            if (inventory.suits.Count > 0)
            SuitAnimator.Play(inventory.CurrentItem(inventory.suits, "suits") + "_Up");
        }
    }

    private void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movement * speed * Time.deltaTime);
    }
}
