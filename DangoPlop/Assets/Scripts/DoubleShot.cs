using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShot : PowerupParent{

	public override void HandlePowerupAction(float powerupLastingTime){
		powerupMaster.startPowerupAction (PowerupType.Double, powerupLastingTime);
	}
}


