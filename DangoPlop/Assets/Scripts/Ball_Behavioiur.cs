using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Behavioiur : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D rb;
    public int thrust;
    private CircleCollider2D circle;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        rb.AddForce(Vector2.right * thrust); 

         
	}
	
	// Update is called once per frame
	void Update () {
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
