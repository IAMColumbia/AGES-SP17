using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void PressedSong1Button()
	{
		Application.OpenURL ("https://opengameart.org/content/elevator-music-2");
	}

	public void PressedSong2Button()
	{
		Application.OpenURL ("https://opengameart.org/content/im-misbehaving");
	}

	public void PressedWoodFloorButton()
	{
		Application.OpenURL ("https://opengameart.org/content/wooden-floortile-seamless-with-normalmap");
	}
}
