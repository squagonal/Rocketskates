using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
[RequireComponent(typeof(IMovementHarness))]
public class BumpMovementAiController : MonoBehaviour,IDamagable
{

    Vector3 direction = Vector3.right;
    [SerializeField]
    float scale = 1;
    IMovementHarness mh;
    [SerializeField]
    List<Vector2> _points;
    int cooldown = 0;
    [SerializeField]
    UnityEvent onBumped;

    // Start is called before the first frame update
    private void Awake()
    {
        mh = GetComponent<IMovementHarness>();
    }

    // Update is called once per frame
    void Update()
    {
        mh.setDirection(direction);
    }
    private void FixedUpdate()
    {
        if (cooldown > 0) cooldown--;

    }
    public void handleDeath()
    {
        Destroy(gameObject, .5f);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (cooldown <= 0)
        {
            List<ContactPoint2D> point2Ds = new List<ContactPoint2D>();
            int i = other.GetContacts(point2Ds);

            _points = point2Ds.Take(i).Select(p => p.point).ToList();
            var ptc = point2Ds.Take(i);
            var flip = ptc.Where(point =>
            {
                var p = (Vector2)transform.position - point.point;
                return Mathf.Abs(p.x) * scale > Mathf.Abs(p.y);
            }).Any();
            if (flip)
            {
                direction *= -1;
                onBumped.Invoke();
                cooldown += 10;
            }
        }

    }

    public void takeDamage()
    {
        handleDeath();
    }
}
