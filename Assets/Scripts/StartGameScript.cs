using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GlobalSettings.SendMessage(MessageType.intro);
	}

}
