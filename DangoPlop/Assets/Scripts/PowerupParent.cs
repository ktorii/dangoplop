using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParent : MonoBehaviour {

	private Rigidbody2D rb2d;
	private Renderer renderer;
	private CircleCollider2D[] colliders;
	static private string playerTag = "Player";
	static private string ballTag = "Ball";
	static private string projectileTag = "Projectile";
	public float timeLandedToDestroy = 3;
	public float groundYPosition;
	public float verticalSpeed;
	public GameObject powerupExplosion;
	public GameObject powerupGlow;
	private float groundTimer = 0.0f;

	// powerup attributes for HandlePowerupAction()
	public float timeLastingPowerup;
	protected GameObject playerObject;
	protected PlayerController playerController;
	// powerupmaster deals with the state of the powerups for the player
	protected PowerupMaster powerupMaster;
	// get the ball spawn factory
	// protected BallFactory ballFactory;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		renderer = GetComponent<Renderer> ();
		colliders = GetComponents<CircleCollider2D> ();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		playerController = playerObject.GetComponent<PlayerController> ();
		powerupMaster = GameObject.FindGameObjectWithTag ("PowerupPanel").GetComponent<PowerupMaster> ();
		// ballFactory = ---;
	}

	void FixedUpdate () 	{
		rb2d.velocity = new Vector2 (0, -verticalSpeed);
	}

	// Update is called once per frame
	void Update () {
		if (groundTimer >= timeLandedToDestroy && playerController.getHasPowerup() == false) {
			// if the powerup has been sitting there without being picked up for the max
			// amount of time, it's safe to destroy since no script is happening for it
			print("gonna destroy now");
			Destroy (gameObject);
		}
		if(gameObject.transform.position.y <= groundYPosition && groundTimer < timeLandedToDestroy){
			// increment timer on ground
			groundTimer += Time.deltaTime;
		}
	}

	// What to do when powerup collides with player
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == playerTag){
			
			print("powerup collision detected");

			// set player state for powerup
			playerController.setHasPowerup (true);

			// particle effects
			Instantiate (powerupExplosion, transform.position, transform.rotation);
			Instantiate (powerupGlow, other.transform.position, other.transform.rotation);

			// delay destroy so that all asynchronous calls in the powerup's 
			// handlepowerupaction function does not get destroyed
			HideGameObject();
			StartCoroutine(DelayDestroy(timeLastingPowerup+1.0f));

			// call appropriate powerup action function
			HandlePowerupAction (timeLastingPowerup);

		} else if (other.tag == ballTag) {
			Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
		} else if (other.tag == projectileTag) {
			Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
		}
	}

	private void HideGameObject() {
		renderer.enabled = false;
		rb2d.isKinematic = true;
		foreach (CircleCollider2D c in colliders) {
			c.enabled = false;
		}
	}

	IEnumerator DelayDestroy(float delay){
		yield return new WaitForSeconds (delay);
		playerController.setHasPowerup (false);
		Destroy (gameObject);
	}

	public virtual void HandlePowerupAction(float powerupLastingTime) {
		print ("powerup parent HandlePowerupAction()");
	}

}
