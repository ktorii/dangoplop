using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParent : MonoBehaviour {

	private Rigidbody2D rb2d;
	static private string playerTag = "Player";
	public float timeLandedToDestroy = 3;
	public float groundYPosition;
	public float verticalSpeed;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		rb2d.velocity = new Vector2 (0, -verticalSpeed);
	}

	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.y <= groundYPosition){
			Destroy(gameObject, timeLandedToDestroy);
		}
	}

	// What to do when powerup collides with player
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == playerTag){
			print("powerup collision detected");
			Destroy(gameObject);
		}
	}

}
