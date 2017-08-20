using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : PowerupParent {
	public override void HandlePowerupAction(float powerupLastingTime){
		powerupMaster.startPowerupAction (PowerupType.Laser, powerupLastingTime);
	}
}
