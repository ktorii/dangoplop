using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speedScale;
	public float maxHorizontalSpeed;
	public float baseJumpPower;
	public float groundYPosition;
    public Animator anim;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
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

		rb2d.velocity = new Vector2 (moveHorizontal, rb2d.velocity.y);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            FindObjectOfType<GameOverMenu>().EndGame();
        }
    }

}
