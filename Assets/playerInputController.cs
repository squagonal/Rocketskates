using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputController : MonoBehaviour
{
    Animator animator;
    Vector2  movementDirection;
    [SerializeField]
    JumpBar jumpBar;
    IMovementHarness  movementHarness; 
    ISkaterHarness playerHarness;
    SpriteRenderer spriteRenderer;
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_JUMP = 2;   
    string currentDirection = "right";
    int currentAnimationState = STATE_IDLE;
    private void Awake() {
    
         movementHarness = gameObject.GetComponent<IMovementHarness>();    
         playerHarness = gameObject.GetComponent<ISkaterHarness>();       
         animator = gameObject.GetComponent<Animator>();
         spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHarness.gravswap();
        movementDirection = new Vector2();
        if (Input.GetKey("w")) {
            if(playerHarness.jumpPower < 15){
            jumpBar.value +=0.04f;
            playerHarness.jumpPower += 0.2f;
            }
        }
        if (Input.GetKeyUp("w")) {
            jumpBar.value = 0;
            movementHarness.Jump();
            playerHarness.jumpPower = 5;
            changeState (STATE_JUMP);
        }
        if (Input.GetKey("s"))
        {
            movementDirection = -playerHarness.transform.up;
        }
        if (Input.GetKey("a"))
        {
            animator.speed += 0.05f;
            changeDirection ("left");
            movementDirection = playerHarness.transform.right;
        }
        if (Input.GetKey("d"))
        {
            animator.speed += 0.05f;
            changeDirection ("right");
            movementDirection = playerHarness.transform.right;
        }
        if (Input.GetKeyUp(KeyCode.Space) && playerHarness.grounded == true)
        {
            playerHarness.gravity = -playerHarness.gravity;
            if (playerHarness.gravity.y < 0){
                spriteRenderer.flipY = true;
            } else {
                spriteRenderer.flipY = false;
            }
        }
        if(movementDirection.x != 0 && playerHarness.grounded){
            playerHarness.skateSpeedUp();
        }
        else 
        {
            playerHarness.skateSpeedDown();
            if(animator.speed >0.3){
            animator.speed -= 0.3f;
            }
        }
        
        movementHarness.setDirection(movementDirection);
        if(playerHarness.grounded == false){
            changeState (STATE_JUMP);
        } else if (playerHarness.speed.x == 0 && playerHarness.speed.y == 0){
            changeState (STATE_IDLE);
        }
    }
     void changeState(int state){
 
        if (currentAnimationState == state)
        return;
 
        switch (state) {
 
        case STATE_WALK:
            animator.SetInteger ("state", STATE_WALK);
            break;
 
        case STATE_JUMP:
            animator.SetInteger ("state", STATE_JUMP);
            break;
 
        case STATE_IDLE:
            animator.SetInteger ("state", STATE_IDLE);
            break;
 
        }
 
        currentAnimationState = state;
    }
    void changeDirection(string direction)
     {
 
         if (currentDirection != direction)
         {
             if (direction == "right")
             {
             transform.Rotate (0, 180, 0);
             currentDirection = "right";
             }
             else if (direction == "left")
             {
             transform.Rotate (0, -180, 0);
             currentDirection = "left";
             }
         }
         if(playerHarness.grounded == true){
             changeState (STATE_WALK);
        }
     }
 
}
