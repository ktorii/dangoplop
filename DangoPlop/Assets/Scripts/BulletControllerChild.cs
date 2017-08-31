using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerChild : BulletController {

	public override void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.CompareTag ("Ceiling") || target.gameObject.CompareTag ("Ball")) {
			if (ammo.bulletType == BulletType.DoubleShot) {
				ammo.currentDoubleShotAmmo++;
			}
			Destroy (gameObject);
		}

	}

}
