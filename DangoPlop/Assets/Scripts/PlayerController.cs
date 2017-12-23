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
	private GameObject ProjectilePos;
	public int Ammo;
    public int maxAmmo;
	public int currentDoubleShotAmmo;
	public int maxDoubleShotAmmo;
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
	private bool alive;

	private AudioSource[] sounds;
	private AudioSource soundShoot;
	private AudioSource soundLaser1;
	private AudioSource soundLaser2;
	private AudioSource soundDeath;





	void Start() {

		rb2d = GetComponent<Rigidbody2D> ();
		collider = GetComponent<CapsuleCollider2D> ();
		ProjectilePos = GameObject.FindGameObjectWithTag("ProjectilePos");
        anim = GetComponent<Animator>();
		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
		originalScale = gameObject.transform.lossyScale;
		originalHeight = gameObject.transform.position.y;
		powerupMaster = GameObject.FindGameObjectWithTag ("PowerupPanel").GetComponent<PowerupMaster> ();
		deadMotion.Set (3.0f, 8.0f);
		collider.isTrigger = false;
		anim.SetBool ("Dead", false);
		change = GameObject.FindGameObjectWithTag ("Ceiling").GetComponent<ChangeBackground> ();
		alive = true;

		// initialize sounds
		sounds = GetComponents<AudioSource> ();
		soundShoot = sounds [0];
		soundLaser1 = sounds [1];
		soundLaser2 = sounds [2];
		soundDeath = sounds [3];

    }


	void Update(){
		if (Input.GetKeyDown(KeyCode.Space) && Ammo > 0 && Time.time > nextFire && alive) {
			Fire ();
			soundShoot.Play ();
			StartCoroutine(Wait());

		}
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


		// if (Ammo > 3 && AmmoReset == true || currentDoubleShotAmmo > 3 && AmmoReset == true) {
		// 	Ammo = 3;
		// 	currentDoubleShotAmmo = 3;
		// }

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
			alive = false;
			soundDeath.Play ();

        }
    }

	public void Fire(){
        switch (bulletType) {
		case BulletType.DoubleShot:
            nextFire = Time.time + DoubleShotRate;
			var doubleShot1 = Instantiate (Projectile2, ProjectilePos.transform.position, Quaternion.identity);
			var doubleShot2 = Instantiate (Projectile3, ProjectilePos.transform.position, Quaternion.identity);
			doubleShot1.transform.Translate (FirstBulletTranslateX, FirstBulletTranslateY, 0, Space.World);
			doubleShot2.transform.Translate (SecondBulletTranslateX, SecondBulletTranslateY, 0, Space.World);
			anim.SetBool("Shot", true);
			Ammo -= 2;
			break;
		case BulletType.Laser:
            nextFire = Time.time + LaserRate;
			Instantiate (Laser, ProjectilePos.transform.position, Quaternion.identity);
			anim.SetBool("Shot", true);
			soundLaser1.Play ();
			soundLaser2.Play ();
			break;
        case BulletType.RapidFire:
            nextFire = Time.time + FireRate;
			Instantiate (Projectile2, ProjectilePos.transform.position, Quaternion.identity);
            anim.SetBool("Shot", true);
            break;
		default:
            nextFire = Time.time + FireRate;
			Instantiate (Projectile, ProjectilePos.transform.position, Quaternion.identity);
			anim.SetBool("Shot", true);
			Ammo--;
			break;
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
        Ammo = 1;
		AmmoReset = false;
	}

	public void doubleShot(){
		bulletType = BulletType.DoubleShot;
		Ammo = maxDoubleShotAmmo;
		AmmoReset = true;

	}

	public void rapidFire(){
        Ammo = 1;
		bulletType = BulletType.RapidFire;
	}

	public void defaultFire(){
        Ammo = maxAmmo;
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
