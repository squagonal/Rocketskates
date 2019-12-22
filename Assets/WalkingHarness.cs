using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingHarness : MonoBehaviour, IMovementHarness
{
    [SerializeField]
    Vector2 force;
    Rigidbody2D rbody;
    [SerializeField]
    Vector2 direction = Vector2.zero;
    public void setDirection(Vector2 direction)
    {
        this.direction=direction;
    }

    // Start is called before the first frame update
    private void Awake() {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position,transform.position+(Vector3)direction,Color.red,0);
    }
    void FixedUpdate()
    {
        rbody.AddForce(direction*force, ForceMode2D.Impulse);
    }
    public void Jump()
    {
        Debug.Log("No Jump 4 u");
    }
}
