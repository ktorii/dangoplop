using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePausedOverlay : MonoBehaviour {
	public GameObject GamePausedUI;

	// Use this for initialization
	void Start () {
		GamePausedUI.SetActive(true);
	}

	// Update is called once per frame
	void Update () {
	}

	public void SetPause (bool input) {
		print("SetPause called with " + input.ToString());
		GamePausedUI.SetActive(input);
	}
}
