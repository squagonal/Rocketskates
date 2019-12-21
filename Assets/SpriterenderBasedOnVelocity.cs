using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class SpriterenderBasedOnVelocity : MonoBehaviour
{
    Rigidbody2D rbody;
    SpriteRenderer sprite_renderer;
    // Start is called before the first frame update
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Round(rbody.velocity.x) != 0)
            sprite_renderer.flipX = rbody.velocity.x < 0;
    }
}
