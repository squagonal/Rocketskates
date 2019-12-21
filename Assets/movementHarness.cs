using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementHarness : MonoBehaviour, IMovementHarness, ISkaterHarness
{
    Vector2  movementDirection;
    Rigidbody2D rigidBody;
    Collider2D collider;
    [SerializeField]
    Vector3 speed;
    public int rotationValue = 0;
    [SerializeField]
    public float jumpPower;
    bool grounded = true;

    float ISkaterHarness.jumpPower { get => jumpPower; set => jumpPower = value; }
    Transform ISkaterHarness.transform { get => transform; }
    int ISkaterHarness.rotationValue { get => rotationValue; set => rotationValue = value; }

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDirection(Vector2 direction){
        movementDirection = direction;
    }

    public void skateSpeedUp(){
        speed.x = 0;
        while(speed.x < 15){
            speed.x += 1;
        }

        
    }
    public void skateSpeedDown(){
        if(speed.x > 0){
           speed.x -= 5;
        }
    }
    public void Jump(){
        if (grounded == true) {
          rigidBody.AddForce(transform.up*((speed.x+3)*jumpPower));
          grounded = false;
      }
    }

    void FixedUpdate()
    {
        collider.enabled = false;
        var hips = Physics2D.Raycast(transform.position, Vector2.down, 1);
        collider.enabled = true;
        Debug.DrawLine(transform.position, transform.position+Vector3.down*1, Color.red, 0.1f);
        if(hips){
            grounded = true; 
        }
        if(grounded == true){
        rigidBody.AddForce(movementDirection*speed);
        } else {
            rigidBody.AddForce(movementDirection*speed/2);
        }
        rigidBody.rotation+= rotationValue;
        Debug.Log(rigidBody.rotation);
    }
}
