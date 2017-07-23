using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public Vector2 Speed;

	Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = Speed;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = Speed;
	}
		

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.CompareTag ("Ceiling") || target.gameObject.tag == "Ball") {
			Destroy (gameObject);
		}
			
	}
		
}
