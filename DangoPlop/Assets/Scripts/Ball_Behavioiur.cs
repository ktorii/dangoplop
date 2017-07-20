using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Behavioiur : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D rb;
    public int thrust;
    private CircleCollider2D circle;
	public float OGBallscaleY;
	public float OGBallscaleX;
	public float MedBallscaleY;
	public float MedBallscaleX;
	public float SmallBallscaleY;
	public float SmallBallscaleX;


	[SerializeField]
	private GameObject OGBall;

	private GameObject ball1, ball2;

	private Ball_Behavioiur ball1Script, ball2Script;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        rb.AddForce(Vector2.right * thrust); 

         
	}
	
	// Update is called once per frame
	void Update () {
        circle.isTrigger = false;
		
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            circle.isTrigger = true;
            
        }
    }

	void InstantiateSplit(){
		if (this.gameObject.tag != "Small Ball" ) {
			ball1 = Instantiate (OGBall);
			ball2 = Instantiate (OGBall);
			ball1Script = ball1.GetComponent<Ball_Behavioiur> ();
			ball2Script = ball2.GetComponent<Ball_Behavioiur> ();
			ball1.GetComponent<Rigidbody2D> ();
			ball2.GetComponent<Rigidbody2D> ();

		}

	}

	void InstantiateBalls(){
		InstantiateSplit ();

		Vector2 temp = transform.position;

  		ball1.transform.position = temp;
		ball1.transform.Translate(-0.4f, 0, 0, Space.World);
		ball2.transform.position = temp;
		ball2.transform.Translate(+0.4f, 0, 0, Space.World);
		var OGballscale = new Vector3 (OGBallscaleX, OGBallscaleY, 1);
		var medballscale = new Vector3 (MedBallscaleX, MedBallscaleY, 1);
		var smallballscale = new Vector3 (SmallBallscaleX, SmallBallscaleY, 1);


		if (gameObject.tag == "OGBall") {
			ball1.transform.localScale = OGballscale;
			ball2.transform.localScale = OGballscale;
			ball1.tag = "Large Ball";
			ball2.tag = "Large Ball";
		}

		if (gameObject.tag == "Large Ball") {
			ball1.transform.localScale = medballscale;
			ball2.transform.localScale = medballscale;
			ball1.tag = "Medium Ball";
			ball2.tag = "Medium Ball";
		}

		if (gameObject.tag == "Medium Ball") {
			ball1.transform.localScale = smallballscale;
			ball2.transform.localScale = smallballscale;
			ball1.tag = "Small Ball";
			ball2.tag = "Small Ball";

		}
	}
		
		

	void OnTriggerEnter2D(Collider2D blip){
		if (blip.tag == "Projectile") {
			if (gameObject.tag != "Small Ball") {
				InstantiateBalls ();
				Destroy (gameObject);
			}
			if (gameObject.tag == "Small Ball") {
				Destroy(gameObject);
			}

		}
	}

		
	
}
