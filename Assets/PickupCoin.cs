using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PickupCoin : MonoBehaviour, IPickup
{
    [SerializeField]
    public float value = 1;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    UnityEngine.Events.UnityEvent onPickup;

    private void Awake()
    {

        spriteRenderer.color = ColorHelper.toRGB(spriteRenderer.color.toHSV() + new Vector4(((value - 1) * 10 / Mathf.PI) % 1, 0, 0, 0));
        onPickup.AddListener(() => Destroy(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var handlers = other.GetComponents<IPickupHandler>();
        bool pickedup = handlers.Any(h => h.pickUp(this));
        if (pickedup)
            onPickup.Invoke();

    }
}
