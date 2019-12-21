using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementHarness : MonoBehaviour
{
    Vector2  movementDirection;
    Rigidbody2D rigidBody;
    [SerializeField]
    Vector2 speed;
    [SerializeField]
    float jumpPower;
    bool grounded = true;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDirection(Vector2 direction){
        movementDirection = direction;
        Debug.Log(movementDirection + "movement harness");
    }

    public void skateSpeedUp(){
        while(speed.x <= 25){
            speed.x += 10;
        }

        
    }
    public void skateSpeedDown(){
        if(speed.x>6){
            speed.x -= 4;
        }
    }
    public void Jump(){
        if (grounded == true) {
          rigidBody.AddForce(transform.up*jumpPower);
          grounded = false;
      }
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(movementDirection*speed);
        if (rigidBody.velocity.y == 0)
        {
            grounded = true;
        }
    }
}
