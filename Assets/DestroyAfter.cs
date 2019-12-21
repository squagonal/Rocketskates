using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float time;
    void Start()
    {
        StartCoroutine(_DestroyAfter(time));
    }

    IEnumerator _DestroyAfter(float t){
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
