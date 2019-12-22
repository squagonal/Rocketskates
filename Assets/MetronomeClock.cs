using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MetronomeClock : MonoBehaviour, IToggleable
{
    [SerializeField]
    Sprite[] tick_repr;
    [SerializeField]
    public float value { private set; get; }
    public float work_done { get => value / (float)(tick_repr.Length*2-1); }


    [SerializeField]
    public float tick_duration;
    [SerializeField]
    public bool is_ticking;
    public bool state => is_ticking;
    [SerializeField]
    public UnityEvent LeftTick, RightTick, Tick;
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(tickHandler());
    }
    public void ToggleState()
    {
        is_ticking = !is_ticking;
    }
    private void Update() {
    }


    IEnumerator tickHandler()
    {
        while (true)
        {
            LeftTickHandler();
            for (int i = 0; i < tick_repr.Length; i++)
            {
                spriteRenderer.sprite = tick_repr[i];
                yield return new WaitForSeconds(tick_duration / tick_repr.Length);
                while (!is_ticking)
                {
                    yield return new WaitForFixedUpdate();
                }
                value = i;
            }
            RightTickHandler();
            for (int i = 0; i < tick_repr.Length; i++)
            {
                spriteRenderer.sprite = tick_repr[tick_repr.Length - 1 - i];
                yield return new WaitForSeconds(tick_duration / tick_repr.Length);
                while (!is_ticking)
                {
                    yield return new WaitForFixedUpdate();
                }
                value += 1;
            }

        }
    }

    void LeftTickHandler()
    {
        Tick.Invoke();
        LeftTick.Invoke();
    }
    void RightTickHandler()
    {
        Tick.Invoke();
        LeftTick.Invoke();
    }
}
