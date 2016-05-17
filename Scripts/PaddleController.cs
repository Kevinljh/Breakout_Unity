//FILE			: PaddleController.cs
//PROJECT		: Gaame development- assignment #1
//PROGRAMMER	: Kevin Li
//FINAL VERSION	: 10/08/2015
//DESCRIPTION	: his class is used to controller the padder. Spawn the ball here 
//				  Press space bar to start. Give the ball a force when the ball hit the paddle.

using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {
	public float speed = 0;
	public float paddleYPosition = 0;
	public float paddleZPosition = 0;
	public float xBoundary = 0;
	public float maxBoundary = 20;
	public float ballSpeed = 4500;
	public GameObject ballPrefab = null;
	public GameObject attachedBall;
	public Rigidbody ballRigidbody;
	public AudioClip paddleSound;
	public AudioSource audio;
	public static PaddleController instance;

	// Use this for initialization
	void Start () {
		spawnBall ();
		audio = GetComponent<AudioSource> ();
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") != 0) {
			transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal")*speed
			                                 *Time.deltaTime, paddleYPosition, paddleZPosition);

			if(transform.position.x < -xBoundary + maxBoundary){
				transform.position = new Vector3(-xBoundary + maxBoundary, paddleYPosition,
				                                 paddleZPosition);
			}
			else if(transform.position.x > xBoundary - maxBoundary){
				transform.position = new Vector3(xBoundary - maxBoundary, paddleYPosition,
				                                 paddleZPosition);
			}
		}

		if (attachedBall) {
			ballRigidbody = attachedBall.GetComponent<Rigidbody>();
			ballRigidbody.position = transform.position + new Vector3(0,7,0);

			if (Input.GetButtonDown("Jump")){
				ballRigidbody.isKinematic = false;
				ballRigidbody.AddForce(0,ballSpeed,0);
				attachedBall = null;
			}
		}
	}

	void spawnBall(){
		attachedBall = Instantiate (ballPrefab, transform.position + new Vector3 (0, 20, 0), 
		                            Quaternion.identity) as GameObject;
	}

	void OnCollisionEnter(Collision col){
		audio.PlayOneShot(paddleSound, 0.1f);
		foreach(ContactPoint contact in col.contacts){
			if(contact.thisCollider == GetComponent<Collider>()){
				print(contact.point.x);
				//This is the paddle contact piont and change the angle 
				float ballangle = contact.point.x - transform.position.x + 10;
				contact.otherCollider.GetComponent<Rigidbody>().AddForce(100*ballangle,0,0);
			}
		}
	}
}
