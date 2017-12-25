using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Factory : MonoBehaviour {

    public GameObject ball;
    public GameObject spawnPos;
    public GameObject spawnPos2;
    private int random;
    public int maxBalls;
    public float spawnWait;
    public static int count;
    public static int smallDeathCount;
    private int targetScore;
    public int scoreIncrement;
    private bool notInLoop;
    public int rangeStart;
    public int rangeEnd;
    private int randomSpeed;
	public static List<GameObject> balls = new List<GameObject> ();
	public float SpawnHeight;
	private Vector2 newPos;
	private Vector2 newPos2;



    // Use this for initialization
    void Start () {
        count = 0;
        smallDeathCount = 0;
        targetScore = scoreIncrement;
        notInLoop = false;
		spawnPos = GameObject.FindGameObjectWithTag ("SpawnOne");
		spawnPos2 = GameObject.FindGameObjectWithTag ("SpawnTwo");
		newPos.Set (spawnPos.transform.position.x, SpawnHeight);
		newPos2.Set (spawnPos2.transform.position.x, SpawnHeight);
		spawnPos.transform.position = newPos;
		spawnPos2.transform.position = newPos2;
		StartCoroutine(SpawnWaves());


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
			spawnWait--;
            targetScore += scoreIncrement;
        }


    }

    IEnumerator SpawnWaves()
    {
        notInLoop = false;
        random = (int)(Random.Range(0, 2));
        randomSpeed = (int)(Random.Range(rangeStart, rangeEnd));
        Ball_Behavioiur.thrust = randomSpeed;
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
            // Debug.Log("DONE RECURSION");
        }
        else
        {
            StartCoroutine(SpawnWaves());
        }


    }

	public void addList(GameObject obj){
		balls.Add (obj);
	}

	public List<GameObject> returnList(){
		return balls;
	}

}
