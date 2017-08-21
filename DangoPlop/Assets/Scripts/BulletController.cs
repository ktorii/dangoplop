using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public Vector2 Speed;

	Rigidbody2D rb;
	private GameObject PlayerControl;
	private PlayerController ammo;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = Speed;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = Speed;
		PlayerControl = GameObject.Find ("Player");
		ammo = PlayerControl.GetComponent<PlayerController> ();
	}
		

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.CompareTag ("Ceiling") || target.gameObject.CompareTag ("Ball")) {
			if (ammo.bulletType == BulletType.DoubleShot) {
				ammo.currentDoubleShotAmmo++;
			}
			if (ammo.bulletType == BulletType.DefaultFire) {
				ammo.Ammo++;
			}
			Destroy (gameObject);
		}

	}
		
}
