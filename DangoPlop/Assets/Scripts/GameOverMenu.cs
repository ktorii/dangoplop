using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public GameObject GameOverUI;
    private bool dead;
    private bool over;

	// Use this for initialization
	void Start () {

        GameOverUI.SetActive(false);
        dead = false;
        Time.timeScale = 1;
        
        		
	}
	
	// Update is called once per frame
	void Update () {
        if (dead)
        {
            over = !over;
        } 

        if (over)
        {
            GameOverUI.SetActive(true);
            Time.timeScale = 0;
        }


    }

    public void EndGame()
    {
        dead = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GamePlay");
        Ball_Factory.count = 0;
        Debug.Log(Ball_Factory.count);
        Time.timeScale = 1;

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Ball_Factory.count = 0;
        Time.timeScale = 1;
    }
}
