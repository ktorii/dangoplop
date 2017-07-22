using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	void Start () {
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	public void AddScore(int newScoreValue) {
	}

	void UpdateScore() {
	}




}