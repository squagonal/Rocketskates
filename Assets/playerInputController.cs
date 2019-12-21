using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputController : MonoBehaviour
{
    Vector2  movementDirection;
    IMovementHarness  movementHarness; 
    // Start is called before the first frame update
    private void Awake() {
    
         movementHarness = gameObject.GetComponent<IMovementHarness>();        
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2();
        if (Input.GetKey("w")) {
            movementHarness.jump();
        }
        if (Input.GetKey("s"))
        {
            movementDirection += Vector2.down;
        }
        if (Input.GetKey("a"))
        {
            movementDirection += Vector2.left;
        }
        if (Input.GetKey("d"))
        {
            movementDirection += Vector2.right;
        }
        movementDirection.Normalize();
        Debug.Log(movementDirection);
        movementHarness.setDirection(movementDirection);
    }
}
