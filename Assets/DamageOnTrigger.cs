using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{

private void OnTriggerEnter2D(Collider2D other){
    var damageObj = other.gameObject.GetComponent<IDamagable>();
    if (damageObj != null){
        damageObj.takeDamage();
    }
}
}
