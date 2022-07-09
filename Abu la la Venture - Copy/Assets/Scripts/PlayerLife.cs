using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private int lives = 3;//initialize the game to have 3 lives
    [SerializeField] private Text livesText;
    [SerializeField] private AudioSource talk;


    private void Start()
    {//get body and animation at the start
        body = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
    }


    //If the user collides with the trap or the enemy. The player will take damage
    //The text will update if the user takes damage
    //Once the live counter reachs 0 the player will be sent back to the previous level
    //Play audio if the player takes damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            lives--;
            livesText.text = "Lives: " + lives;
            talk.Play();


            if (lives == 0)
            {
                Die();
            }
        }

        else if (collision.gameObject.CompareTag("Enemy"))//doesnt work yet
        {
            lives--;
            livesText.text = "Lives: " + lives;
            talk.Play();

            if (lives == 0)
            {
                Die();
            }

        }
    }

    //change the player sprite to death sprite
    private void Die()
    {
        body.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    //once the player dies the player goes back to the previous level
    private void RestartLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


}