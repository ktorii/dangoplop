﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerChild2 : BulletController {

	public override void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.CompareTag ("Ceiling") || target.gameObject.CompareTag ("Ball") && ammo != null) {
			if (ammo.bulletType == BulletType.DoubleShot) {
				ammo.Ammo++;
			}
            Destroy (gameObject);
		}

	}
}
