using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissappear : MonoBehaviour, IToggleable
{
    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]
    Collider2D col;
    [SerializeField]
    bool is_solid = true;
    public bool state => is_solid;

    public void ToggleState()
    {
        is_solid = !is_solid;
        col.enabled = is_solid;
        sr.color = is_solid ? sr.color.setA(1) : sr.color.setA(.5f);
    }

 
}
