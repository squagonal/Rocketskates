using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class crushDetector : MonoBehaviour
{
    [SerializeField]
    float width;
    [SerializeField]
    UnityEvent onCrush;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit= Physics2D.Raycast(transform.position+Vector3.left*width,Vector3.right,width*2);   
        if (hit)
        {
            Debug.DrawLine(transform.position+Vector3.left*width,transform.position+Vector3.right*width, Color.green,0);
            onCrush.Invoke();
        }
        else
        {
            Debug.DrawLine(transform.position+Vector3.left*width,transform.position+Vector3.right*width, Color.red,0);
        }
    }
}
