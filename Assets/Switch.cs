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
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ToggleButton()
    {
        is_active = !is_active;
        if (is_active)
        {
            spriteRenderer.color = active_color;
            spriteRenderer.sprite = active_image;
            onActivate.Invoke();
        }
        else
        {
            spriteRenderer.color = inactive_color;
            spriteRenderer.sprite = inactive_image;
            onDeactivate.Invoke();
        }
    }

    [SerializeField]
    UnityEvent onActivate;
    [SerializeField]
    UnityEvent onDeactivate;
    int i = 0;
    [SerializeField]
    bool is_weighted = false;
    [SerializeField,Range(0,float.MaxValue)]
    float weighted_delay = 10;
    Coroutine cooldown_handler;

    Vector4 target_hue;
    float w;
    IEnumerator cooldown()
    {
        for (float w = 0; w < weighted_delay; w+=weighted_delay/10)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = ColorHelper.toRGB(Vector4.Lerp(spriteRenderer.color.toHSV(), target_hue, 1/(weighted_delay+0.001f)));
        }
        ToggleButton();
    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cooldown_handler != null)
            StopCoroutine(cooldown_handler);
        if (!(is_weighted && is_active))
            ToggleButton();
        i++;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        i--;
        if (is_weighted && i <= 0)
        {
            target_hue = active_color.toHSV();
            target_hue.x = inactive_color.toHSV().x;
            w=0;
        if (cooldown_handler == null)
            cooldown_handler = StartCoroutine(cooldown());
        }
    }
}
