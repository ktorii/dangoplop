using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
//	public GUIText scoreText;

	// Use this for initialization
	void Start () {
//		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
		
	}
	public void AddScore(int newScoreValue) {
//		scorer = blah;
//		UpdateScore();
	}
	void UpdateScore() {
//		scoreText.text = "Score: " + score;
	}
//	IEnumerator SpawnWaves() {
//		yield return new WaitForSeconds (200);
//	}
}