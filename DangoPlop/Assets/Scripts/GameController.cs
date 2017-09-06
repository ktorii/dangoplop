using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private bool isGameRunning = false;
	// music starts only for first pause
	private bool firstPauseInitiated = false;
	public GameObject musicManagerObject;
	private MusicManager musicManagerScript;

	void Start () {
		musicManagerScript = musicManagerObject.GetComponent<MusicManager> ();
	}

	void Update () {
		UpdateGameState();

		if (Input.GetKeyDown (KeyCode.P)) {
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
		FindObjectOfType<PlayerController>().GetComponent<Animator>().updateMode =
			AnimatorUpdateMode.Normal;
	}
}