using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementHarness : MonoBehaviour, IMovementHarness, ISkaterHarness
{
    Vector2  movementDirection;
    Rigidbody2D rigidBody;
    Collider2D col;
    [SerializeField]
    Vector3 speed;
    Vector3 ISkaterHarness.speed { get => speed; set => speed = value; }
    [SerializeField]
    Vector3 gravity;
    Vector3 ISkaterHarness.gravity { get => gravity; set => gravity = value; }
    [SerializeField]
    Vector2 gravDir;
    public int rotationValue = 0;
    [SerializeField]
    public float jumpPower;
    bool grounded = true;
    bool ISkaterHarness.grounded { get => grounded; set => grounded = value; }

    float ISkaterHarness.jumpPower { get => jumpPower; set => jumpPower = value; }
    Transform ISkaterHarness.transform { get => transform; }
    int ISkaterHarness.rotationValue { get => rotationValue; set => rotationValue = value; }

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
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
        while(speed.x < 25){
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
          rigidBody.AddForce(-gravDir*((speed.x*20+1)+jumpPower));
      }
    }

    void FixedUpdate()
    {
        gravDir = -transform.up;
        col.enabled = false;
        var hips = Physics2D.Raycast(transform.position, -transform.up, 1);
        var head = Physics2D.Raycast(transform.position, transform.up, 1);
        col.enabled = true;
        Debug.DrawLine(transform.position, transform.position+Vector3.down*1, Color.red, 0.1f);
        if(hips || head){
            grounded = true; 
        } else { 
            grounded = false;
        }
        if(grounded == true){
            rigidBody.AddForce(movementDirection*speed);
        } else {
            rigidBody.AddForce(movementDirection*speed/2);
        }
        rigidBody.rotation+= rotationValue;
        Debug.Log(grounded);
    }
    void ISkaterHarness.gravswap(){
        rigidBody.AddForce(gravDir*gravity);
    }
}
