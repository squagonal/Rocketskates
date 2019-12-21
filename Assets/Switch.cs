using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    [SerializeField]
    public bool is_active { private set; get; }
    [SerializeField]
    Sprite active_image, inactive_image;
    [SerializeField]
    Color active_color, inactive_color;
    private void Awake() {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    public void ToggleButton()
    {
        is_active = !is_active;
        if (is_active)
        {
            spriteRenderer.color=active_color;
            spriteRenderer.sprite=active_image;
            onActivate.Invoke();
        }
        else
        {
            spriteRenderer.color=inactive_color;
            spriteRenderer.sprite=inactive_image;
            onDeactivate.Invoke();
        }
    }

    [SerializeField]
    UnityEvent onActivate;
    [SerializeField]
    UnityEvent onDeactivate;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        ToggleButton();
    }
}
