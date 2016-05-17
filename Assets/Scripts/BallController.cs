//FILE			: BallController.cs
//PROJECT		: Gaame development- assignment #1
//PROGRAMMER	: Kevin Li
//FINAL VERSION	: 10/08/2015
//DESCRIPTION	: This class is used to controll the ball. Distory bricks when the ball hits it, at the same time,
//				  add score or reduce lives.


using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public float speed = 4500;
	public GameObject ball;
	public GameObject block;
	public AudioClip bricksound;
	public AudioClip wallsound;
	private Rigidbody rb;
	private bool firstHitMid = false;
	private bool firstHitTop = false;
	private int hitWall = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -100) {
			//decrese live
			GameController.lives--;
			restBall();
		}

		if (GameController.lives == 0) {
			Application.LoadLevel("Gameover");
		}

		if (GameObject.FindGameObjectsWithTag ("Block").Length < 1) {
			if(GameController.round == GameController.roundLimit){
				Application.LoadLevel("Won");
			}
			else{
				GameController.instance.createBlocks();
				restBall();
				GameController.round++;
			}
		}


	}

	void restBall(){
		//ball bie
		Vector3 temp = transform.position;
		temp.y = GameObject.FindGameObjectWithTag("Paddle").transform.position.y+7f;
		temp.x = GameObject.FindGameObjectWithTag("Paddle").transform.position.x;
		transform.position = temp;
		ball.GetComponent<Rigidbody>().Sleep();
		
		//clone the paddle object and attacted the ball from paddlecontroller
		PaddleController pController;
		pController = GameObject.FindGameObjectWithTag("Paddle")
			.GetComponent<PaddleController>();
		pController.attachedBall = this.gameObject;
		//reset paddel
		GameObject.FindGameObjectWithTag("Paddle").transform.localScale = new Vector3(40,4,5);
		PaddleController.instance.maxBoundary = 20;
		//reset speed
		rb.mass = 1f;
	}

	void OnCollisionEnter(Collision col){
		//play sound and distory ball
		if (col.gameObject.tag == "Block") {
			//GetComponent<AudioSource>().PlayOneShot(bricksound, 0.5);
			Destroy(col.gameObject);
			hitWall = 0;
			//increase score
			if(GameController.score >= 0){
				//increse score for different level
				if(col.transform.position.y < 32){
					GameController.score++;
				}
				else if(col.transform.position.y > 43){
					GameController.score = GameController.score + 5;
					if(!firstHitTop){rb.mass = 0.5f; firstHitTop=true;};
				}
				else{
					GameController.score = GameController.score + 3;
					if(!firstHitMid){rb.mass=0.8f; firstHitMid=true;}
				}
			}
		}

		if (col.gameObject.name == "Top Wall") {
			GameObject.FindGameObjectWithTag("Paddle").transform.localScale = new Vector3(20,4,5);
			PaddleController.instance.maxBoundary = 10;
		}

		//play sound when ball hits wall
		if (col.gameObject.tag == "Wall") {
			//GetComponent<AudioSource>().PlayOneShot(wallsound, 0.5);
			if(hitWall==2){
				rb.AddForce(transform.position);
			}
			hitWall++;
		}

	}
}
