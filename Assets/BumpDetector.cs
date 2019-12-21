using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BumpDetector : MonoBehaviour
{
    [SerializeField]
    int cooldown;

    [SerializeField]
    private float scale;
    [SerializeField]
    UnityEvent bumped;
    [SerializeField]
    List<Vector2> points;
    [SerializeField]
    BumpMovementAiController aiController;
    bool has_bumped = false;

    // Start is called before the first frame update
    void Awake()
    {
        aiController= GetComponent<BumpMovementAiController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cooldown > 0) cooldown--;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (cooldown <= 0)
        {
            List<ContactPoint2D> point2Ds = new List<ContactPoint2D>();
            int i = other.GetContacts(point2Ds);
            points = point2Ds.Take(i).Select(jvalue => jvalue.point).ToList();
            var flip = point2Ds.Take(i).Where(point =>
            {
                var p = (Vector2)transform.position - point.point;
                return Mathf.Abs(p.x) * scale > Mathf.Abs(p.y);
            }).Any();
            if (flip)
            {
                cooldown += 100;
                bumped.Invoke();
                has_bumped = true;
            }
        }
        else
        {
            has_bumped = false;
        }

    }
}
