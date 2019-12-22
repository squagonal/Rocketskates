using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicDestroyable : MonoBehaviour, IDamagable
{
    [SerializeField]
    public UnityEvent onDeath, onDamage;
    public void handleDeath()
    {
        onDeath.Invoke();
    }

    public void takeDamage()
    {
        onDamage.Invoke();
    }

    public void Destroy_This(float t)
    {
        Destroy(gameObject, t);
    }

}
