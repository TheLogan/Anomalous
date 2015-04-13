using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public GameObject Following; 
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = Following.transform.position;
	}
}
