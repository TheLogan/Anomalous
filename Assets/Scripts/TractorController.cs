using UnityEngine;
using System.Collections;

public class TractorController : CenterStats
{
    private float speed;
    public KeyCode TracKey = KeyCode.Mouse0;
    public GameObject TracBeam;
    private Vector2 lastpostion;
    public SoundPlayer sPlayer;

    public float Speed
    {
        get { return speed; }
    }

    void Update()
    {
	    if (Time.timeScale <= 0)
	    {
		    TracBeam.SetActive(false);
			sPlayer.Stop(true);
	    }else if (Input.GetKeyDown(TracKey))
        {
			TracBeam.SetActive(true);
			sPlayer.Play();
        }else if (Input.GetKeyUp(TracKey))
        {
            TracBeam.SetActive(false);
            pullingObject.Clear();
            sPlayer.Stop(true);
        }
		

		
        speed = Vector2.Distance(transform.position,lastpostion);
        lastpostion = transform.position;
    }
}