using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParent : MonoBehaviour {

	private Rigidbody2D rb2d;
	static private string playerTag = "Player";
	static private string ballTag = "Ball";
	static private string projectileTag = "Projectile";
	public float timeLandedToDestroy = 3;
	private GameObject ground;
	private float groundYPosition;
	public float verticalSpeed;
	public GameObject powerupExplosion;
	public GameObject powerupGlow;
	private float groundTimer = 0.0f;

	// powerup attributes for HandlePowerupAction()
	public float timeLastingPowerup;

	// powerupmaster deals with the state of the powerups for the player
	protected PowerupMaster powerupMaster;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		powerupMaster = GameObject.FindGameObjectWithTag ("PowerupPanel").GetComponent<PowerupMaster> ();
		ground = GameObject.FindGameObjectWithTag ("Ground");
		groundYPosition = ground.transform.position.y + (ground.GetComponent<BoxCollider2D> ().size.y/2);
		print (groundYPosition);
	}

	void FixedUpdate () 	{
		rb2d.velocity = new Vector2 (0, -verticalSpeed);
	}

	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.y <= groundYPosition){
			Destroy (gameObject, timeLandedToDestroy);
		}
	}

	// What to do when powerup collides with player
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == playerTag){
			
			print("powerup collision detected");

			// particle effects
			Instantiate (powerupExplosion, transform.position, transform.rotation);
			Instantiate (powerupGlow, other.transform.position, other.transform.rotation);

			// call appropriate powerup action function
			HandlePowerupAction (timeLastingPowerup);

			// DESTROY the powerup
			Destroy(gameObject);

		} else if (other.tag == ballTag) {
			Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
		} else if (other.tag == projectileTag) {
			Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
		}
	}

	public virtual void HandlePowerupAction(float powerupLastingTime) {
		print ("powerup parent HandlePowerupAction()");
	}

}
