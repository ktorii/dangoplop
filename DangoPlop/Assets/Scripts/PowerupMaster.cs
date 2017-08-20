using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType {
	Empty,
	Shrink,
	Bomb,
	Double,
	Grow,
	Laser,
	Life,
	Random,
	Rapidfire,
	Time
}

/*
 * Description: PowerupMaster is the god that overlooks the powerups in the game.
 *              It has access to the player, ballfactory, and poweruppanel.
 *              The powerupMaster has all knowledge of the state of the active powerups and controls
 * 				the logic that deals with them.
 * Important:	Powerups do not pile on top of each other. If I get a shrink powerup and then a 
 * 				double shot powerup, I only have a double shot powerup and I'm not shrunk anymore
 * 				(this is easily implementable though with how Ken set this script up)
 */

public class PowerupMaster : MonoBehaviour {

	public Sprite powerupShrink;
	public Sprite powerupBomb;
	public Sprite powerupDouble;
	public Sprite powerupGrow;
	public Sprite powerupLaser;
	public Sprite powerupLife;
	public Sprite powerupRandom;
	public Sprite powerupRapidfire;
	public Sprite powerupTime;

	private PowerupType activePowerup = PowerupType.Empty;
	// ignore this for now
	private int powerupLevel = 1;
	private float activePowerupTimer = 0.0f;
	private float activePowerupTimeLimit = 0.0f;

	private SpriteRenderer renderComponent;

	private GameObject playerObject;
	private PlayerController playerController;
	private Ball_Factory ballFactory;

	// Use this for initialization
	void Start () {
		renderComponent = GetComponent<SpriteRenderer> ();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		playerController = playerObject.GetComponent<PlayerController> ();
		ballFactory = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Ball_Factory> ();
	}
	
	// Update is called once per frame
	void Update () {
		// if there is a time limit set for the powerup, then check if the 
		if (isActivePowerupTimeLimitSet()) {
			if (activePowerupTimer >= activePowerupTimeLimit) {
				stopPreviousActivePowerup ();
			}
		}
		if (activePowerup != PowerupType.Empty) {
			activePowerupTimer += Time.deltaTime;
		}
	}

	public void startPowerupAction(PowerupType incomingPowerupType, float powerupLastingTime) {
		//TODO next version: if get 2 powerups same, then do level 2 powerup. For now just extend lifetime of active
		//if (activePowerup == incomingPowerupType) {
		//	not yet
		//}

		// remove previous powerup if new one came
		if (activePowerup != PowerupType.Empty) {
			stopPreviousActivePowerup ();
			// notice if powerup is the same, we basically remove the previous powerup and add the same powerup back
		}

		// set new active powerup
		activePowerup = incomingPowerupType;

		switch (incomingPowerupType) {
		case PowerupType.Shrink:
			// change sprite image on top right
			renderComponent.sprite = powerupShrink;

			ShrinkPlayer ();
			break;
		case PowerupType.Bomb:
			renderComponent.sprite = powerupBomb;
			break;
		case PowerupType.Double:
			renderComponent.sprite = powerupDouble;
			break;
		case PowerupType.Grow:
			renderComponent.sprite = powerupGrow;
			GrowPlayer ();
			break;
		case PowerupType.Laser:
			renderComponent.sprite = powerupLaser;
			break;
		case PowerupType.Life:
			renderComponent.sprite = powerupLife;
			break;
		case PowerupType.Random:
			renderComponent.sprite = powerupRandom;
			break;
		case PowerupType.Rapidfire:
			renderComponent.sprite = powerupRapidfire;
			break;
		case PowerupType.Time:
			renderComponent.sprite = powerupTime;
			break;
		default:
			break;
		}

		// Set new active powerup time limit. if the powerup has no reversible action,
		// then just set back the active powerup to nothing right away
		if (powerupLastingTime != 0f && powerupLastingTime != null) {
			activePowerupTimeLimit = powerupLastingTime;
		} else {
			resetActivePowerupSpecs ();
		}
	}

	public void stopShowingPowerup() {
		renderComponent.sprite = null;
	}

	private bool isActivePowerupTimeLimitSet() {
		return (activePowerupTimeLimit != null && activePowerupTimeLimit > 0.0f);
	}

	private void stopPreviousActivePowerup() {
		switch (activePowerup) {
		case PowerupType.Shrink:
			ResetPlayerSize();
			break;
		case PowerupType.Bomb:
			break;
		case PowerupType.Double:
			break;
		case PowerupType.Grow:
			ResetPlayerSize();
			break;
		case PowerupType.Laser:
			break;
		case PowerupType.Life:
			break;
		case PowerupType.Random:
			break;
		case PowerupType.Rapidfire:
			break;
		case PowerupType.Time:
			break;
		default:
			break;
		}

		// remove active powerup stats
		resetActivePowerupSpecs ();

		// remove active powerup panel
		stopShowingPowerup();
	}

	private void resetActivePowerupSpecs() {
		activePowerup = PowerupType.Empty;
		activePowerupTimer = 0.0f;
		activePowerupTimeLimit = 0.0f;
	}

	public PowerupType getActivePowerup() {
		return activePowerup;
	}

	private void ShrinkPlayer() {
		Vector3 originalScale = playerController.getOriginalScale ();
		playerObject.transform.localScale = new Vector3 (originalScale.x / 2, originalScale.y / 2, originalScale.z / 2);
		Vector3 originalPosition = playerObject.transform.position;
	}

	private void ResetPlayerSize() {
		Vector3 originalScale = playerController.getOriginalScale ();
		float originalHeight = playerController.getOriginalHeight ();
		playerObject.transform.localScale = originalScale;
	}

	private void GrowPlayer() {
		Vector3 originalScale = playerController.getOriginalScale ();
		playerObject.transform.localScale = new Vector3 (originalScale.x * 2, originalScale.y * 2, originalScale.z * 2);
		Vector3 originalPosition = playerObject.transform.position;
	}

	// MAKE YOUR POWERUP FUNCTIONS HERE
}
