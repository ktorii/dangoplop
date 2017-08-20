using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : PowerupParent {
	public override void HandlePowerupAction(float powerupLastingTime){
		powerupMaster.startPowerupAction (PowerupType.Rapidfire, powerupLastingTime);
	}
}
