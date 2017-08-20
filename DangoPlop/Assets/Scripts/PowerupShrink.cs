using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupShrink : PowerupParent {
	// override the powerup action function. This is the function that gets called 
	// when the player gets the powerup.
	public override void HandlePowerupAction(float powerupLastingTime){
		powerupMaster.startPowerupAction (PowerupType.Shrink, powerupLastingTime);
	}
}
