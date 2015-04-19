using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void PlayGame()
	{
		Application.LoadLevel("GameLevel");
	}

	public void Exit()
	{
		Application.Quit();
	}
}
