using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed=5;
    Rigidbody2D rigidbody;
    Vector2 movement;
    public Animator PlayerAnimator;
    public bool isMoving = false;
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();    
    }
    // Update is called once per frame
    void Update()
    {
        if (!isMoving) 
        {
            PlayerAnimator.Play("Body_Idle");
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
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
        }
        if (movement.x > 0)
        {
            PlayerAnimator.Play("Body_Walking_Right");
        }
        if (movement.y < 0)
        {
            PlayerAnimator.Play("Body_Walking_Down");
        }
        if (movement.y > 0)
        {
            PlayerAnimator.Play("Body_Walking_Up");
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * speed * Time.deltaTime);
    }
}
