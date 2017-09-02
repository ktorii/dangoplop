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
	public float speedScale;
	public float maxHorizontalSpeed;
	public float baseJumpPower;
	public float groundYPosition;

	public Animator anim;
	private Vector3 originalScale;
	private float originalHeight;

	// projectiles
	public GameObject Projectile;
	public GameObject Laser;
	private Transform ProjectilePos;

	// Ammo contains the number of shots the player has left to use for their current bullet
	public static int defaultAmmoMax = 3;
	public int doubleShotAmmoMax = 3;
	public int rapidShotAmmoMax = int.MaxValue;
	public int Ammo = defaultAmmoMax;
	public BulletType currentBulletType = BulletType.DefaultFire;

	// fire rates
	public float FireRate = 0F;
	public float DoubleShotRate = 0.5F;
	public float LaserRate = 2F;
	public float nextFire = 0.0F;

	// double shot powerup
	public float FirstBulletTranslateX = -0.3F;
	public float FirstBulletTranslateY = 0F;
	public float SecondBulletTranslateX = 0.3F;
	public float SecondBulletTranslateY = 0F;

	public bool AmmoReset = false;
	// laser to freeze player
	public bool Froze;
	private PowerupMaster powerupMaster;

	private int times = 0;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		ProjectilePos = transform.Find ("BulletPos");
        anim = GetComponent<Animator>();
		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
		originalScale = gameObject.transform.lossyScale;
		originalHeight = gameObject.transform.position.y;
		powerupMaster = GameObject.FindGameObjectWithTag ("PowerupPanel").GetComponent<PowerupMaster> ();
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

		bool hasInfiniteAmmo = (currentBulletType == BulletType.RapidFire || currentBulletType == BulletType.Laser);
		if (Input.GetKeyDown(KeyCode.X) && (Ammo > 0 || hasInfiniteAmmo) && Time.time > nextFire) {
			Fire ();
			print ("fired!" + times);
			times++; // delete this once you are done. for debugging.
			StartCoroutine(Wait());
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
           FindObjectOfType<GameOverMenu>().EndGame();
        }
    }

	public void Fire(){
		if (currentBulletType == BulletType.DefaultFire) {
			nextFire = Time.time + FireRate;
			Instantiate (Projectile, ProjectilePos.position, Quaternion.identity);
			anim.SetBool("Shot", true);
			Ammo--;
		} 
		else if (currentBulletType == BulletType.Laser) {
			nextFire = Time.time + LaserRate;
			Instantiate (Laser, ProjectilePos.position, Quaternion.identity);
			anim.SetBool("Shot", true);
			Ammo--;
		} 
		else if (currentBulletType == BulletType.DoubleShot) {
			nextFire = Time.time + DoubleShotRate;
			var doubleShot1 = Instantiate (Projectile, ProjectilePos.position, Quaternion.identity);
			var doubleShot2 = Instantiate (Projectile, ProjectilePos.position, Quaternion.identity);
			doubleShot1.transform.Translate (FirstBulletTranslateX, FirstBulletTranslateY, 0, Space.World);
			doubleShot2.transform.Translate (SecondBulletTranslateX, SecondBulletTranslateY, 0, Space.World);
			anim.SetBool("Shot", true);
			Ammo--;
		} 
		else if (currentBulletType == BulletType.RapidFire) {
			nextFire = Time.time + FireRate;
			Instantiate (Projectile, ProjectilePos.position, Quaternion.identity);
			anim.SetBool("Shot", true);
		}
	}

	public Vector3 getOriginalScale() {	
		return originalScale;
	}

	public float getOriginalHeight() {
		return originalHeight;
	}


	// functions to set the state of the bullet
	public void setBulletType(BulletType newBulletType) {
		switch (newBulletType) {
		case BulletType.Laser:
			currentBulletType = BulletType.Laser;
//			AmmoReset = false;
			break;
		case BulletType.DoubleShot:
			currentBulletType = BulletType.DoubleShot;
			Ammo = doubleShotAmmoMax;
//			AmmoReset = true;
			break;
		case BulletType.RapidFire:
			currentBulletType = BulletType.RapidFire;
			Ammo = rapidShotAmmoMax;
			break;
		case BulletType.DefaultFire:
			currentBulletType = BulletType.DefaultFire;
			Ammo = defaultAmmoMax;
			print ("reset to default");
//			AmmoReset = true;
			break;
		default:
			break;
		}
	}

	public void incrementAmmo() {
		switch (currentBulletType) {
		case BulletType.Laser:
			break;
		case BulletType.DoubleShot:
			if (Ammo < doubleShotAmmoMax) {
				Ammo++;
			}
			break;
		case BulletType.RapidFire:
			if (Ammo < rapidShotAmmoMax) {
				Ammo++;
			}
			break;
		case BulletType.DefaultFire:
			if (Ammo < defaultAmmoMax) {
				Ammo++;
			}
			break;
		default:
			break;
		}
	}

	public void laser(){
		
	}
	public void doubleShot(){
		

	}
	public void rapidFire(){
		
	}
	public void defaultFire(){
		

	}
	
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
		if (currentBulletType == BulletType.Laser) {
			LaserStopTime LaserTime = GameObject.FindGameObjectWithTag ("Projectile").GetComponent<LaserStopTime> ();
			Froze = true;
			yield return new WaitForSeconds (LaserRate = LaserTime.LaserTime());
			speedScale = 4;
			Froze = false;
		}
        anim.SetBool("Shot", false);
    }
}
