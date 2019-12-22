using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lazer : MonoBehaviour, IToggleable
{
    // Start is called before the first frame update
    float distance = 100;
    LineRenderer lr;
    [SerializeField]
    bool is_active = false;

    public bool state => is_active;

    private void Awake()
    {
        lr = GetComponentsInChildren<LineRenderer>().ElementAtOrDefault(0);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (is_active)
        {
            lr.enabled = true;
            var hit = Physics2D.Raycast(transform.position, transform.up, distance);
            Debug.DrawRay(transform.position, transform.up * distance, Color.blue, 0.1f);
            if (hit)
            {
                lr.SetPositions(new Vector3[] { transform.position, hit.point });
                var damagable = hit.transform.GetComponent<IDamagable>();
                if (damagable != null)
                    damagable.takeDamage();
            }
            else
                lr.SetPositions(new Vector3[] { transform.position, transform.position + transform.up * distance });
        }
        else
            lr.enabled = false;
    }

    public void ToggleState()
    {
        is_active = !is_active;
    }
}
