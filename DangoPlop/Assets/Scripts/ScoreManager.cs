using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int Score;     


	public Text score;
	public Text highscore;


	 void Start ()
	{
		highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore", Score).ToString();	
		score = GetComponent <Text> ();
		Score = 0;	
	}


	void Update ()
	{
		score.text = "Score: " + Score;
		if (Score > PlayerPrefs.GetInt("HighScore")){
			PlayerPrefs.SetInt ("HighScore", Score);
			PlayerPrefs.Save ();
		}

	}
}

