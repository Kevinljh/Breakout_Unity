//Here is a code which all you need is to copy and paste it in c# file amd you will have a menu then attach this script to the Main Camera in the scene and you can pick if you want a background image to add to it:


//FILE			: Menu.cs
//PROJECT		: Gaame development- assignment #1
//PROGRAMMER	: Kevin Li
//FINAL VERSION	: 10/08/2015
//DESCRIPTION	: //This class is used to show menu whick will show up first, you can start game form here or see "about" the game.

using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public GUISkin guiSkin;
	public Texture2D background, LOGO;
	public bool DragWindow = false;
	public string levelToLoadWhenClickedPlay = "";
	public string[] AboutTextLines = new string[0];
	
	
	private string clicked = "", MessageDisplayOnAbout = "About \n ";
	private Rect WindowRect = new Rect((Screen.width / 2)-250, (Screen.height / 2)-200, 500, 150);
	private float volume = 1.0f;
	
	private void Start()
	{
		for (int x = 0; x < AboutTextLines.Length;x++ )
		{
			MessageDisplayOnAbout += AboutTextLines[x] + " \n ";
		}
		MessageDisplayOnAbout += "Name : Kevin Li \n Course : Simulation and Game Development \n Assignment : Breakout \n";
		MessageDisplayOnAbout += "\n\n\nHow to play? Using a single ball, \n the player must knock down as many \nbricks as possible by " +
			"using the \n walls and/or the paddle below to ricochet \n the ball against the bricks and eliminate \nthem. If the player's " +
				"paddle misses \n the ball's rebound, he or she loses a \n turn. Bricks in the lowest 2 rows are worth \n one point each. " +
				"The middle row bricks \n are worth 3 points each and the top 2 \n rows have bricks worth 5 points each. \n\n\n" + "Press esc to go back";
	}
	
	private void OnGUI()
	{
		if (background != null)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height),background);
		if (LOGO != null && clicked != "about")
			GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), LOGO);
		
		GUI.skin = guiSkin;
		if (clicked == "")
		{
			WindowRect = GUI.Window(0, WindowRect, menuFunc, "Main Menu");
		}
		else if (clicked == "options")
		{
			WindowRect = GUI.Window(1, WindowRect, optionsFunc, "Options");
		}
		else if (clicked == "about")
		{
			GUI.Box(new Rect (0,0,Screen.width,Screen.height), MessageDisplayOnAbout);
		}else if (clicked == "resolution")
		{
			GUILayout.BeginVertical();
			for (int x = 0; x < Screen.resolutions.Length;x++ )
			{
				if (GUILayout.Button(Screen.resolutions[x].width + "X" + Screen.resolutions[x].height))
				{
					Screen.SetResolution(Screen.resolutions[x].width,Screen.resolutions[x].height,true);
				}
			}
			GUILayout.EndVertical();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Back"))
			{
				clicked = "options";
			}
			GUILayout.EndHorizontal();
		}
	}
	
	private void optionsFunc(int id)
	{
		if (GUILayout.Button("Resolution"))
		{
			clicked = "resolution";
		}
		GUILayout.Box("Volume");
		volume = GUILayout.HorizontalSlider(volume ,0.0f,1.0f);
		AudioListener.volume = volume;
		if (GUILayout.Button("Back"))
		{
			clicked = "";
		}
		if (DragWindow)
			GUI.DragWindow(new Rect (0,0,Screen.width,Screen.height));
	}
	
	private void menuFunc(int id)
	{
		//buttons 
		if (GUILayout.Button("Play Game"))
		{
			//play game is clicked
			Application.LoadLevel("Game");
		}
		if (GUILayout.Button("Options"))
		{
			clicked = "options";
		}
		if (GUILayout.Button("About"))
		{
			clicked = "about";
		}
		if (GUILayout.Button("Quit Game"))
		{
			Application.Quit();
		}
		if (DragWindow)
			GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
	}
	
	private void Update()
	{
		if (clicked == "about" && Input.GetKey (KeyCode.Escape))
			clicked = "";
	}
}