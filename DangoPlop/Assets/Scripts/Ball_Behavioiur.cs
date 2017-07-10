using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Behavioiur : MonoBehaviour {
    public GameObject player;
    public GameObject ball;
    public Vector2 ballPos;
    public Rigidbody2D rb;
    public int thrust;
    public CircleCollider2D circle;
    public int counter;

    // Use this for initialization
    void Start () {
        ball = GameObject.FindGameObjectWithTag("Ball");
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        thrust = 100;
        rb.AddForce(Vector2.right * thrust); 

         
	}
	
	// Update is called once per frame
	void Update () {
        ballPos = ball.transform.position;
        circle.isTrigger = false;
		
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            circle.isTrigger = true;
            
        }
    }
}
