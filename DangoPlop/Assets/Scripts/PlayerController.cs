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
	public int Ammo = 3;
    public Animator anim;

	private Vector3 originalScale;
	private float originalHeight;
	private bool hasPowerup = false;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		ProjectilePos = transform.Find("ProjectilePos");
        anim = GetComponent<Animator>();
		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
		originalScale = gameObject.transform.lossyScale;
		originalHeight = gameObject.transform.position.y;
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
		if (Input.GetKeyDown (KeyCode.Space) && Ammo > 0) {
			Ammo--;
			Fire ();
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

	void Fire(){

		Instantiate (Projectile, ProjectilePos.position, Quaternion.identity);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
           FindObjectOfType<GameOverMenu>().EndGame();
        }
    }

	public Vector3 getOriginalScale() {
		return originalScale;
	}

	public float getOriginalHeight() {
		return originalHeight;
	}

	public bool getHasPowerup() {
		return hasPowerup;
	}

	public void setHasPowerup(bool newState) {
		hasPowerup = newState;
	}
}
