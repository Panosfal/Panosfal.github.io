using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private Rigidbody2D rb;
    public bool grounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float MoveX=Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveX * speed, rb.velocity.y);

        if(Input.GetKey(KeyCode.Space) && grounded){
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        grounded = false;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground"){
            grounded = true;
        }
    }
}
