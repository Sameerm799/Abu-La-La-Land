using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


/*If the user is in the cast point of the enemy, loop the animation to trigger the attack while pushing the enemy into death
colliding contact the enemy will push the characters body into the edges of the platform causing the player to die
while also considering to take one health away from the player on contact. 
ex: if the player contacts the enemy -1 life taken, and if the player is still colliding with the enemy the enemy will push the character to the edges it is facing
if player has 0 lives die go back to start screen*/
public class Enemy_Attack : MonoBehaviour
{
    
    //variable can be changed in unity
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] Transform player;
    [SerializeField] Transform castPoint;

    //vaiables set in script
    private bool isFacingLeft;
    private float attackCooldown = 0;
    private float cooldownTimer = Mathf.Infinity;
    private float agroRange = 0;

    //calling unity references
    private PlayerLife playerHealth;
    
    private Animator animation;

    private void Awake(){
        animation = GetComponent<Animator>();
    }

    void Start(){
      

    }
    private void Update(){
        cooldownTimer += Time.deltaTime;
      if (EnemyVision(agroRange)){
          AttackTrigger();
          if(HostileInSight()){
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animation.SetTrigger("enemy_hostile");
                
                
            }
        }
      }
   
   
    }

//change the direction of the enemy sprite if facing left or right
    void AttackTrigger(){
        if (transform.position.x < player.position.x)
        {
           
            transform.localScale = new Vector2(1,1);
            isFacingLeft = false;
        }
        else{
           
            transform.localScale = new Vector2(-1,1);
            isFacingLeft = true;
        }

        animation.SetTrigger("enemy_hostile");
    }





//check if the enemy is in line of sight, if the tag that it is colliding with is player
//return true, else false and continue movements
    bool EnemyVision(float distance){
        bool val = false;
        float castDist = distance;

        if (isFacingLeft){
             castDist = -distance;
        }

        Vector2 endPos = castPoint.position +Vector3.right ;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider !=null)
        {

            if (hit.collider.gameObject.CompareTag ("Player")){
                val = true;
            }
            else{
                val = false;
            }

            
        }
        else{
            
        }

        return val;
    }


    
//once the hostile is in site and in collision deplete the players life by one and activate 
//trigger activation animation
    private bool HostileInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range *transform.localScale.x *colliderDistance, new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left,0,playerLayer);

        if(hit.collider != null){
            playerHealth = hit.transform.GetComponent<PlayerLife>();
            AttackTrigger();
        }
      

        return hit.collider != null;



    

    }

//if the player has no more lives, damage the player and go back one scene.
    private void DamagePlayer(){
        if (HostileInSight()){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
