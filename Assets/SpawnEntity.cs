using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    [SerializeField]
    GameObject spawnable;
    [SerializeField]
    public bool should_spawn = true;
    public void Spawn()
    {
        if (should_spawn)
            Instantiate(spawnable,this.transform.position,this.transform.rotation,this.transform);
    }
}
