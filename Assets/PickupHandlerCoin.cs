using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandlerCoin : MonoBehaviour, IPickupHandler
{
    [SerializeField]
    float score =0;
    public bool pickUp(IPickup p)
    {
        var coin = p as PickupCoin;
        if(coin)
        {
            score+=coin.value;
            return true;
        }
        return false;
    }

 
}
