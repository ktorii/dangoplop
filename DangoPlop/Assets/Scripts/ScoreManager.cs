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
        if (Score >= 500 && Score < 1000) {
            score.color = new Color(0.0f/255.0f, 102.0f/255.0f, 0.0f/255.0f);
        }
        else if (Score >= 1000 && Score < 2000) {
            score.color = new Color(0.0f/255.0f, 153.0f/255.0f, 153.0f/255.0f);
            
        }
        else if (Score >= 2000 && Score < 3000) {
            score.color = new Color(0.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
            
        }
        else if (Score >= 3000 && Score < 4000) {
            score.color = new Color(255.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
        }
        else if (Score >= 4000 && Score < 5000) {
            score.color = new Color(255.0f/255.0f, 102.0f/255.0f, 0.0f/255.0f);
        }
        else if (Score >= 5000) {
            score.color = new Color(255.0f/255.0f, 0.0f/255.0f, 0.0f/255.0f);
        }

	}
}

