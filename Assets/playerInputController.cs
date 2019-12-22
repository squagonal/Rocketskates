using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputController : MonoBehaviour
{
    Vector2  movementDirection;
    [SerializeField]
    JumpBar jumpBar;
    IMovementHarness  movementHarness; 
    ISkaterHarness playerHarness;
    // Start is called before the first frame update
    private void Awake() {
    
         movementHarness = gameObject.GetComponent<IMovementHarness>();    
         playerHarness = gameObject.GetComponent<ISkaterHarness>();       
    }

    // Update is called once per frame
    void Update()
    {
        playerHarness.gravswap();
        movementDirection = new Vector2();
        if (Input.GetKey("w")) {
            if(playerHarness.jumpPower < 15000){
            jumpBar.value +=0.04f;
            playerHarness.jumpPower += 0.2f;
            }
        }
        if (Input.GetKeyUp("w")) {
            jumpBar.value = 0;
            movementHarness.Jump();
            //playerHarness.jumpPower = 5;
        }
        if (Input.GetKey("s"))
        {
            movementDirection = -playerHarness.transform.up;
        }
        if (Input.GetKey("a"))
        {
            movementDirection = -playerHarness.transform.right;
        }
        if (Input.GetKey("d"))
        {
            movementDirection = playerHarness.transform.right;
        }
        if (Input.GetKeyUp(KeyCode.Space) && playerHarness.grounded == true)
        {
            playerHarness.gravity = -playerHarness.gravity;
        }
        if(movementDirection.x != 0){
            playerHarness.skateSpeedUp();
        }
        else 
        {
            playerHarness.skateSpeedDown();
        }
        
        movementHarness.setDirection(movementDirection);
    }
}
