using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDoubleShot : PowerupParent{
	public float limitDoubleShotAmmo;

	public override void HandlePowerupAction(float powerupLastingTime){
		powerupMaster.startPowerupAction (PowerupType.Double, powerupLastingTime);
		playerController.currentDoubleShotAmmo = limitDoubleShotAmmo;

	}
}


