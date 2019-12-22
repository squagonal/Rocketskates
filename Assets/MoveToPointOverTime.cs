using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPointOverTime : MonoBehaviour
{
    [SerializeField]
    public Transform destination;
    [SerializeField]
    public float time_to_destination;
    public MetronomeClock metronome;
    private void Awake() {
        metronome = transform.GetComponentInParent<MetronomeClock>();
    }
    // Update is called once per frame
    void Update()
    {
//time_to_destination=metronome.tick_duration;
        transform.position = Vector3.Lerp(transform.position, destination.position, time_to_destination * Time.deltaTime);
        
    }
    public void setDestination(Transform destination)
    { this.destination = destination; }
}
