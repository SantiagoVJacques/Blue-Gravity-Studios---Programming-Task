using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed=5;
    Rigidbody2D rigidbody;
    Vector2 movement;
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();    
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * speed * Time.deltaTime);
    }
}
