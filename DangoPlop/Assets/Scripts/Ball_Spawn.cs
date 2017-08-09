using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Spawn : MonoBehaviour {

    public GameObject ball;
    public GameObject spawnPos;
    public GameObject spawnPos2;
    private int random;
    public int maxBalls;
    public float spawnWait;
    public static int count;
    public static int smallDeathCount;
    private int targetScore;
    private bool notInLoop;

    // Use this for initialization
    void Start () {
        count = 0;
        smallDeathCount = 0;
        StartCoroutine(SpawnWaves());
        targetScore = 1000;
        notInLoop = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (count < maxBalls && notInLoop)
        {
            StartCoroutine(SpawnWaves());
        }

        if (ScoreManager.Score >= targetScore)
        {
            maxBalls++;
            targetScore += 1000;
        }
        
    }

    IEnumerator SpawnWaves()
    {
        notInLoop = false;
        random = (int)(Random.Range(0, 2));
        if (random == 0)
        {          
            Instantiate(ball, spawnPos.transform.position, transform.rotation);
        }
        else
        {
            Instantiate(ball, spawnPos2.transform.position, transform.rotation);
        }   
        count++;
        yield return new WaitForSeconds(spawnWait);
        if (count == maxBalls)
        {
            notInLoop = true;
            Debug.Log("DONE RECURSION");
        }
        else
        {            
            StartCoroutine(SpawnWaves());
        }


    }

}
