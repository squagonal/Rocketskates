using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputController : MonoBehaviour
{
    Vector2  movementDirection;
    [SerializeField]
    JumpBar jumpBar;
    movementHarness  movementHarness; 
    // Start is called before the first frame update
    void Start()
    {
         movementHarness = gameObject.GetComponent<movementHarness>();      
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2();
        if (Input.GetKey("w")) {
            if(movementHarness.jumpPower < 15){
            jumpBar.value +=0.04f;
            movementHarness.jumpPower += 0.2f;
            }
        }
        if (Input.GetKeyUp("w" ) || Input.GetKeyUp(KeyCode.Space)) {
            jumpBar.value = 0;
            movementHarness.jump();
            movementHarness.jumpPower = 5;
        }
        if (Input.GetKey("s"))
        {
            movementDirection = -movementHarness.transform.up;
        }
        if (Input.GetKey("a"))
        {
            movementDirection = -movementHarness.transform.right;
        }
        if (Input.GetKey("d"))
        {
            movementDirection = movementHarness.transform.right;
        }
        if (Input.GetKey("e"))
        {
            movementHarness.rotationValue = 1;
        }
        if (Input.GetKeyUp("e"))
        {
            movementHarness.rotationValue = 0;
        }
        if (Input.GetKey("q"))
        {
            movementHarness.rotationValue = -1;
        }
        if (Input.GetKeyUp("q"))
        {
            movementHarness.rotationValue = 0;
        }
        if(movementDirection.x != 0){
            movementHarness.skateSpeedUp();
        }
        else 
        {
            movementHarness.skateSpeedDown();
        }
        
        movementHarness.setDirection(movementDirection);
    }
}
