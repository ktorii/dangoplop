using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType {
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

	private PowerupType activePowerup;
	// ignore this for now
	private int powerupLevel = 1;

	private SpriteRenderer renderComponent;

	// Use this for initialization
	void Start () {
		renderComponent = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowPowerup(PowerupType powerupType) {
		switch (powerupType) {
		case PowerupType.Shrink:
			renderComponent.sprite = powerupShrink;
			break;
		case PowerupType.Bomb:
			renderComponent.sprite = powerupBomb;
			break;
		case PowerupType.Double:
			renderComponent.sprite = powerupDouble;
			break;
		case PowerupType.Grow:
			renderComponent.sprite = powerupGrow;
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
	}

	public void StopShowingPowerup() {
		renderComponent.sprite = null;
	}
}
