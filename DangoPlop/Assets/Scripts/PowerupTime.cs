using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTime : PowerupParent {
	public override void HandlePowerupAction(float powerupLastingTime){
		powerupMaster.startPowerupAction (PowerupType.Time, powerupLastingTime);
	}
}