using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupFactory : MonoBehaviour {
    // should really make a map of the powerups, but wtv
    public GameObject powerupBomb;
    public GameObject powerupDouble;
    public GameObject powerupGrow;
    public GameObject powerupLaser;
    public GameObject powerupRapidfire;
    public GameObject powerupShrink;
    public GameObject powerupTime;
    public GameObject powerupLife;
    public Vector2 spawnOffset;
    public float chanceOfSpawnPowerup;
    public int scoreToIncreaseChance;
    public float moreChancePerScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnPowerup(Vector3 position, Quaternion rotation) {
        // don't spawn anything if lucky
        float randNum = Random.Range(0.0f, 1.0f);
        // Debug.Log(ScoreManager.Score);
        // Debug.Log(scoreToIncreaseChance);
        // Debug.Log((int)(ScoreManager.Score / scoreToIncreaseChance));
        // Debug.Log((int)(ScoreManager.Score / scoreToIncreaseChance) * moreChancePerScore);
        if (randNum > chanceOfSpawnPowerup + ((int)(ScoreManager.Score / scoreToIncreaseChance) * moreChancePerScore)) {
            return;
        }

        Vector3 spawnPosition = position;
        spawnPosition.x += spawnOffset.x;
        spawnPosition.y += spawnOffset.y;

        // choose random powerup type
        int randInt = Random.Range(0, 7);

        // instantiate powerup
		switch (randInt) {
		case 0:
            Instantiate(powerupBomb, spawnPosition, rotation);
			break;
		case 1:
			Instantiate(powerupDouble, spawnPosition, rotation);
			break;
		case 2:
			Instantiate(powerupGrow, spawnPosition, rotation);
			break;
		case 3:
			Instantiate(powerupLaser, spawnPosition, rotation);
			break;
		case 4:
			Instantiate(powerupRapidfire, spawnPosition, rotation);
			break;
		case 5:
			Instantiate(powerupShrink, spawnPosition, rotation);
			break;
		case 6:
            // Debug.Log("time");
			Instantiate(powerupTime, spawnPosition, rotation);
			break;
    	// case PowerupType.Life:
		// 	renderComponent.sprite = powerupLife;
		// 	break;
		// case PowerupType.Random:
		// 	renderComponent.sprite = powerupRandom;
		// 	break;
		default:
			break;
		}
	}
}
