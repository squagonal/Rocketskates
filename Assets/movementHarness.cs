using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementHarness : MonoBehaviour
{
    Vector2  movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2();
        if (Input.KeyDown("w")) {
            movementDirection += Vector2.up;
        }
        if (Input.KeyDown("s"))
        {
            movementDirection += Vector2.down;
        }
        if (Input.KeyDown("a"))
        {
            movementDirection += Vector2.left;
        }
        if (Input.KeyDown("d"))
        {
            movementDirection += Vector2.right;
        }
        movementDirection.Normalise();
        Debug.log(movementDirection);
    }

    void FixedUpdate()
    {

    }
}
