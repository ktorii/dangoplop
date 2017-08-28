using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SizeType
{
	Ball,
	LargeBall,
	MediumBall,
	SmallBall

}



public class Ball_Behavioiur : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D rb;
	private Rigidbody2D ball1Speed;
	private Rigidbody2D ball2Speed;
    public static int thrust;
    private CircleCollider2D circle;
	public GameObject Ball;
	private GameObject Projectile;
	private SizeType type = SizeType.LargeBall;
	public int LargeScoreValue = 10;
	public int MedScoreValue = 20;
	public int SmallScoreValue = 30;
	public float LargeBallScale;
	public float MedBallScale;
	public float SmallBallScale;
	public float Ball1TranslateX;
	public float Ball2TranslateX;
	public float Ball1TranslateY;
	public float Ball2TranslateY;
	public bool GameStart = true;
	public GameObject ballExplosion;
    public Vector2 position;
    public double maxHeight;
    public double sHeight;
    public double mHeight;
    public double lHeight;
	public Vector2 newSpeed;
	private GameObject ground;
	private double distance;
	private Ball_Factory ballFactory;

    

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        position = rb.transform.position;
		if (type == SizeType.LargeBall) {
			maxHeight = lHeight;
		}
		newSpeed.Set (0,0);
		ground = GameObject.FindGameObjectWithTag ("Ground");
		BoxCollider2D thickness = ground.GetComponent<BoxCollider2D> ();
		distance = maxHeight-(ground.transform.position.y + (thickness.size.y/2));
		ballFactory = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Ball_Factory> ();
		ballFactory.addList (this.gameObject);


			
        

    }
		
	// Update is called once per frame
	void Update () {
        circle.isTrigger = false;
		var largeballscale = new Vector3 (LargeBallScale, LargeBallScale, 1);
		if (GameStart == true) {
			rb.AddForce(Vector2.right * thrust);
			Ball.transform.localScale = largeballscale;
			GameStart = false;
		}


        position = rb.transform.position;

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
			circle.isTrigger = true;
        }

		if (coll.gameObject.tag == "Ground") {
			newSpeed.Set((float)rb.velocity.x,  Mathf.Sqrt ((float)(2 * 9.8 * distance)));
			rb.velocity = newSpeed;
		}
    }

	private void BallExplosion() {
		Instantiate (ballExplosion, transform.position, transform.rotation);
	}

	public void HandleSplit(){
		Projectile = GameObject.FindGameObjectWithTag ("Projectile");

		if (type != SizeType.SmallBall) {
			
			var ball1Obj = Instantiate (Ball);
			var ball2Obj = Instantiate (Ball);
			Ball_Behavioiur ball1 = ball1Obj.GetComponent<Ball_Behavioiur> ();
			Ball_Behavioiur ball2 = ball2Obj.GetComponent<Ball_Behavioiur> ();
			ball1Speed = ball1Obj.GetComponent<Rigidbody2D> ();
			ball2Speed = ball2Obj.GetComponent<Rigidbody2D> ();
			ball1Speed.AddForce (Vector2.left * thrust);
			ball2Speed.AddForce (Vector2.right * thrust);

			Vector2 temp = transform.position;
			ball1Obj.transform.position = temp;
			ball1Obj.transform.Translate (Ball1TranslateX, Ball1TranslateY, 0, Space.World);
			ball2Obj.transform.position = temp;
			ball2Obj.transform.Translate (Ball2TranslateX, Ball2TranslateY, 0, Space.World);
			var medballscale = new Vector3 (MedBallScale, MedBallScale, 1);
			var smallballscale = new Vector3 (SmallBallScale, SmallBallScale, 1);

			if (type == SizeType.LargeBall) {
				ball1Obj.transform.localScale = medballscale;
				ball2Obj.transform.localScale = medballscale;
				ball1.type = SizeType.MediumBall;
				ball2.type = SizeType.MediumBall;
				ball1.LargeScoreValue = MedScoreValue;
				ball2.LargeScoreValue = MedScoreValue;
                ball1.mediumHeight();
                ball2.mediumHeight();
				


            }

			else if (type == SizeType.MediumBall) {
				ball1Obj.transform.localScale = smallballscale;
				ball2Obj.transform.localScale = smallballscale;
				ball1.type = SizeType.SmallBall;
				ball2.type = SizeType.SmallBall;
				ball1.LargeScoreValue = SmallScoreValue;
				ball2.LargeScoreValue = SmallScoreValue;
                ball1.smallHeight();
                ball2.smallHeight();

            }

		}
		if (Projectile == true) {
			Destroy (gameObject);
		}
	}




	void OnTriggerEnter2D(Collider2D blip){
		if (blip.gameObject.tag == "Projectile") {
			ScoreManager.Score += LargeScoreValue;
			HandleSplit ();
			BallExplosion ();
		}
	}

    private void OnDestroy()
    {
        if (type == SizeType.SmallBall)
        {
            Ball_Factory.smallDeathCount++;
        }
        if (Ball_Factory.smallDeathCount == 4)
        {
            Ball_Factory.count--;
            Ball_Factory.smallDeathCount = 0;
        }
		Ball_Factory.balls.Remove (this.gameObject);
    }

	public void largeHeight()
	{
		maxHeight = lHeight;
	}

    public void mediumHeight()
    {
        maxHeight = mHeight;
    }
    public void smallHeight()
    {
        maxHeight = sHeight;
    }

}
