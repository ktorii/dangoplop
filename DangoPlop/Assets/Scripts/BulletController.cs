using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public Vector2 Speed;

	protected Rigidbody2D rb;
	protected GameObject PlayerControl;
	protected PlayerController ammo;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = Speed;
		PlayerControl = GameObject.Find ("Player");
		ammo = PlayerControl.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	public virtual void OnTriggerEnter2D(Collider2D target){
		print (ammo);
		bool test = (target.gameObject.CompareTag ("Ceiling") || target.gameObject.CompareTag ("Ball") && ammo != null);
		print (test);

		if (target.gameObject.CompareTag ("Ceiling") || target.gameObject.CompareTag ("Ball") && ammo != null) {
//			if (ammo.currentBulletType == BulletType.DefaultFire) {
				ammo.incrementAmmo();
//			}
			Destroy (gameObject);
		}

	}
		
}
