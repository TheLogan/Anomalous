using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalSettings.SendMessage(MessageTypes.win);
	}
}
