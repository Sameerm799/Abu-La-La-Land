using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D hitbox;
    private SpriteRenderer sprite; 
    

    [SerializeField] private LayerMask jumpableGround;

    //the move speed and jumpforce of the player
    private float dirX = 0f;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 7f;

    private enum MovementState { idle, running, jummping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();//reference to body componenet
        hitbox = GetComponent<BoxCollider2D>();//reference to box collider, detecting the players collision
        sprite = GetComponent<SpriteRenderer>();//sprite: Goat Image
       
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //raw means theres no sliding aka back to zero 
        body.velocity = new Vector2(dirX * moveSpeed, body.velocity.y); //multiply since dirX can be negative for left or positive for right
        
        
        //only allow the player to jump if they have touched the ground, stops user from spamming jump key
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }

        UpdateSprite();
    }

    //Change the sprite of the goat depending on the way the player is facing and death
    private void UpdateSprite()
    {

        if (dirX > 0f)
        {
            
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
           
            sprite.flipX = true; 
        }
 

        
    }

    //check if the player is touching the ground 
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(hitbox.bounds.center, hitbox.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}