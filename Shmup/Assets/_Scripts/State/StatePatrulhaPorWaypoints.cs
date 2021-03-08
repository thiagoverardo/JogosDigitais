using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrulhaPorWaypoints : State
{
    public Transform[] waypoints;  
    Rigidbody2D rb;

    public override void Awake()
    {
        base.Awake();
        // Configure a transição para outro estado aqui.
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        waypoints[0].position = transform.position;
        waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
    }

    public override void Update()
    {
        if(Vector3.Distance(transform.position, waypoints[1].position) > .1f) {
            Vector3 direction = waypoints[1].position - transform.position;
            direction.Normalize();
            rb.MovePosition(rb.position + new Vector2(direction.x, direction.y) * Time.fixedDeltaTime);
        } else {
            waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
        }
    }
 
}