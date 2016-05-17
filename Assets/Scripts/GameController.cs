//FILE			: GameContoller.cs
//PROJECT		: Gaame development- assignment #1
//PROGRAMMER	: Kevin Li
//FINAL VERSION	: 10/08/2015
//DESCRIPTION	: This class is the game controller which create the game ui and bricks. All the game data like 
//				  score and lives will keep here.

using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject block;
	public GameObject block1;
	public GameObject block2;
	public static int lives = 5;
	public static int score = 0;
	public static int round = 1;
	public static int roundLimit = 2;
	public static GameController instance;

	// Use this for initialization
	void Start () {
		instance = this;
		createBlocks ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.color = Color.white;
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.label);
		myButtonStyle.fontSize = 20;
		GUI.Label(new Rect(80,10,100,50), "Score:" + score, myButtonStyle);		
		GUI.Label(new Rect(180,10,100,50), "Lives:" + lives, myButtonStyle);		
		GUI.Label(new Rect(290,10,100,50), "Round:" + round,myButtonStyle);
		if (GUI.Button (new Rect (20, 10, 50, 30), "Menu")) {
			Application.LoadLevel("Menu");
		}
	}

	public void createBlocks(){

		//Instantiate(block, new Vector3(21, 54, 0), Quaternion.identity);
		//Instantiate(block, new Vector3(51, 54, 0), Quaternion.identity);
		for (int i = -5; i < 6; i++) {

			Instantiate(block, new Vector3(i*23, 10, 0), Quaternion.identity);
		}
		for (int i = -5; i < 6; i++) {
			Instantiate(block, new Vector3(i*23, 21, 0), Quaternion.identity);
		}
		for (int i = -5; i < 6; i++) {
			Instantiate(block1, new Vector3(i*23, 32, 0), Quaternion.identity);
		}
		for (int i = -5; i < 6; i++) {
			Instantiate(block1, new Vector3(i*23, 43, 0), Quaternion.identity);
		}
		for (int i = -5; i < 6; i++) {
			Instantiate(block2, new Vector3(i*23, 54, 0), Quaternion.identity);
		}
		for (int i = -5; i < 6; i++) {
			Instantiate(block2, new Vector3(i*23, 65, 0), Quaternion.identity);
		}

	}
	
}
