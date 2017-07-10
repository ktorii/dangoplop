using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speedScale;
	public float maxHorizontalSpeed;
	public float baseJumpPower;
	public float groundYPosition;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	void FixedUpdate() {

		float moveVertical = Input.GetAxis ("Vertical");
		// dango can only jump if it's on the ground
		if(moveVertical > 0 && rb2d.position.y <= groundYPosition){
			moveVertical = baseJumpPower;
		}
		else{
			moveVertical = 0;
		}

		Vector2 verticalMoveDelta = new Vector2 (0, moveVertical);
		rb2d.AddForce (verticalMoveDelta, ForceMode2D.Impulse);

		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector2 horizontalMoveDelta = new Vector2 (moveHorizontal, 0);

		rb2d.AddForce (horizontalMoveDelta * speedScale, ForceMode2D.Impulse);

		// limit dango's horizontal speed
		if(rb2d.velocity.x > maxHorizontalSpeed){
			rb2d.velocity.Set (maxHorizontalSpeed, rb2d.velocity.y);
		}
	}

}
