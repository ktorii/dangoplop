using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupShrink : PowerupParent {
	// remember playerObject, playerController script, ballFactory, and timeLastingPowerup
	// were inherited from PowerupParent

	// override the powerup action function. This is the function that gets called 
	// when the player gets the powerup.
	public override void HandlePowerupAction(float powerupLastingTime){
		powerupMaster.ShowPowerup (PowerupType.Shrink);
		ShrinkPlayer ();
		// delay unshrink for powerupLastingTime
		Invoke ("UnShrinkPlayer", powerupLastingTime);
	}

	private void ShrinkPlayer() {
		Vector3 originalScale = playerController.getOriginalScale ();
		print (originalScale);
		playerObject.transform.localScale = new Vector3 (originalScale.x / 2, originalScale.y / 2, originalScale.z / 2);
		Vector3 originalPosition = playerObject.transform.position;
//		gameObject.transform.position.Set (originalPosition.x, groundYPosition + originalScale.y / 2, originalPosition.z);
	}

	private void UnShrinkPlayer() {
		print ("in unshrink");
		Vector3 originalScale = playerController.getOriginalScale ();
		print (originalScale);
		float originalHeight = playerController.getOriginalHeight ();
		playerObject.transform.localScale = originalScale;
//		Vector3 prevPosition = playerObject.transform.position;
//		playerObject.transform.position.Set (prevPosition.x, originalHeight, prevPosition.z);
		powerupMaster.StopShowingPowerup();
	}
}
