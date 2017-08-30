using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType
{
	DefaultFire,
	RapidFire,
	DoubleShot,
	Laser,
}

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	private CapsuleCollider2D collider;
	public float speedScale;
	public float maxHorizontalSpeed;
	public float baseJumpPower;
	public float groundYPosition;
	public GameObject Projectile;
	public GameObject Projectile2;
	public GameObject Projectile3;
	public GameObject Laser;
	private Transform ProjectilePos;
	public int Ammo = 3;
	public float currentDoubleShotAmmo;
	public float maxDoubleShotAmmo;
    public Animator anim;
	private Vector3 originalScale;
	private float originalHeight;
	public float FireRate = 0F;
	public float DoubleShotRate = 0.5F;
	public float LaserRate = 2F;
	public float nextFire = 0.0F;
	public float FirstBulletTranslateX = -0.3F;
	public float FirstBulletTranslateY = 0F;
	public float SecondBulletTranslateX = 0.3F;
	public float SecondBulletTranslateY = 0F;
	public BulletType bulletType = BulletType.DefaultFire;
	public bool AmmoReset = false;
	public bool Froze;
	private PowerupMaster powerupMaster;
	private Vector2 deadMotion;
	private ChangeBackground change;





	void Start() {

		rb2d = GetComponent<Rigidbody2D> ();
		collider = GetComponent<CapsuleCollider2D> ();
		ProjectilePos = transform.Find ("ProjectilePos");
        anim = GetComponent<Animator>();
		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
		originalScale = gameObject.transform.lossyScale;
		originalHeight = gameObject.transform.position.y;
		powerupMaster = GameObject.FindGameObjectWithTag ("PowerupPanel").GetComponent<PowerupMaster> ();
		deadMotion.Set (3.0f, 8.0f);
		collider.isTrigger = false;
		anim.SetBool ("Dead", false);
		change = GameObject.FindGameObjectWithTag ("Ceiling").GetComponent<ChangeBackground> ();

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


		if (Input.GetKeyDown(KeyCode.Space) && Ammo > 0 && Time.time > nextFire) {
			Fire ();
			StartCoroutine(Wait());

		}

		if (Ammo > 3 && AmmoReset == true || currentDoubleShotAmmo > 3 & AmmoReset == true) {
			Ammo = 3;
			currentDoubleShotAmmo = 3;
		}

		rb2d.velocity = new Vector2 (moveHorizontal, rb2d.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }

		if (Froze) {
			speedScale = 0;
		}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
			Debug.Log ("dead");
			anim.SetBool ("Dead", true);
			collider.isTrigger = true;
           FindObjectOfType<GameOverMenu>().EndGame();
			change.changeBackground ();
			rb2d.velocity = deadMotion;


        }
    }

	public void Fire(){
		if (bulletType == BulletType.DefaultFire) {
			nextFire = Time.time + FireRate;
			Instantiate (Projectile, ProjectilePos.position, Quaternion.identity);
			anim.SetBool("Shot", true);
			Ammo--;
		}
		else if (bulletType == BulletType.Laser) {
			nextFire = Time.time + LaserRate;
			Instantiate (Laser, ProjectilePos.position, Quaternion.identity);
			anim.SetBool("Shot", true);
			Ammo--;
		}
		else if (bulletType == BulletType.DoubleShot && currentDoubleShotAmmo > 0) {
			nextFire = Time.time + DoubleShotRate;
			var doubleShot1 = Instantiate (Projectile2, ProjectilePos.position, Quaternion.identity);
			var doubleShot2 = Instantiate (Projectile3, ProjectilePos.position, Quaternion.identity);
			doubleShot1.transform.Translate (FirstBulletTranslateX, FirstBulletTranslateY, 0, Space.World);
			doubleShot2.transform.Translate (SecondBulletTranslateX, SecondBulletTranslateY, 0, Space.World);
			anim.SetBool("Shot", true);
			currentDoubleShotAmmo--;
		}
		else if (bulletType == BulletType.RapidFire) {
			nextFire = Time.time + FireRate;
			Instantiate (Projectile2, ProjectilePos.position, Quaternion.identity);

		}
	}

	public Vector3 getOriginalScale() {
		return originalScale;
	}

	public float getOriginalHeight() {
		return originalHeight;
	}

	public void laser(){
		bulletType = BulletType.Laser;
		AmmoReset = false;
	}

	public void doubleShot(){
		bulletType = BulletType.DoubleShot;
		currentDoubleShotAmmo = maxDoubleShotAmmo;
		AmmoReset = true;
	}

	public void rapidFire(){
		bulletType = BulletType.RapidFire;
	}

	public void defaultFire(){
		bulletType = BulletType.DefaultFire;
		AmmoReset = true;

	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
		if (bulletType == BulletType.Laser) {
			LaserStopTime LaserTime = GameObject.FindGameObjectWithTag ("Projectile").GetComponent<LaserStopTime> ();
			Froze = true;
			yield return new WaitForSeconds (LaserRate = LaserTime.LaserTime());
			speedScale = 4;
			Froze = false;
		}
        anim.SetBool("Shot", false);
    }
}
