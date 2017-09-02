using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBomb : PowerupParent {
	public override void HandlePowerupAction(float powerupLastingTime){
		powerupMaster.startPowerupAction (PowerupType.Bomb, powerupLastingTime);
	}
}