using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speedScale;
	public float maxHorizontalSpeed;
	public float baseJumpPower;
	public float groundYPosition;
	public GameObject Projectile;
	private Transform ProjectilePos;
	public float fireRate = 0.5F;
	private float nextFire = 0.0F;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		ProjectilePos = transform.Find("ProjectilePos");
	}
	void FixedUpdate() {

		float moveVertical = Input.GetAxis ("Vertical");
		// dango can only jump if it's on the ground
		if(moveVertical > 0 && rb2d.position.y <= groundYPosition) {
			moveVertical = baseJumpPower;
		}
		else {
			moveVertical = 0;
		}

		Vector2 verticalMoveDelta = new Vector2 (0, moveVertical);
		rb2d.AddForce (verticalMoveDelta, ForceMode2D.Impulse);

		float moveHorizontal = Input.GetAxis ("Horizontal") * speedScale;

		// limit dango's horizontal speed
		if(moveHorizontal > maxHorizontalSpeed) {
			moveHorizontal = maxHorizontalSpeed;
		}
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Fire ();
		}

		rb2d.velocity = new Vector2 (moveHorizontal, rb2d.velocity.y);
	}
	void Fire(){

		Instantiate (Projectile, ProjectilePos.position, Quaternion.identity);
	}


}
