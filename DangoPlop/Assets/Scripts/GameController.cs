using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private bool isGameRunning = false;

	void Start () {
	}

	void Update () {
		UpdateGameState();

		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		} else if (Input.GetKeyDown (KeyCode.P)) {
			TogglePause();
		}
	}

	public void AddScore(int newScoreValue) {
	}

	void UpdateScore() {
	}

	void UpdateGameState() {
		if( isGameRunning ) {
			Time.timeScale = 1.0f;
		} else {
			Time.timeScale = 0.0f;
		}
	}

	void TogglePause() {
		isGameRunning = !isGameRunning;
		FindObjectOfType<GamePausedOverlay>().SetPause(!isGameRunning);
	}
}