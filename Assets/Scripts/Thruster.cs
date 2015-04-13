using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour
{
    public SoundPlayer sPlayer;
	public ThrusterStruct[] MyThrusters;

    void Update()
    {
        bool soundPlay = false;
	    foreach (var thrusters in MyThrusters)
	    {
		    bool isActive = false;

		    foreach (var key in thrusters.ActivatorKeys)
		    {
			    if (Input.GetKey(key))
			    {
				    isActive = true;
			    }
		    }

		    foreach (var thruster in thrusters.Particles)
		    {
                if (isActive)
                {
                    if (!thruster.isPlaying)
                    {
                        thruster.Play();
                      
                    }
                }
                else
                {
                    thruster.Stop();
                   
                }
			}

            if (isActive)
                soundPlay = true;
	    }

        if (soundPlay)
        {
            sPlayer.Play();
        }
        else
        {
            sPlayer.Stop(true);
        }
    }
}
	[Serializable]
	public class ThrusterStruct
	{
		public List<KeyCode> ActivatorKeys;
		public List<ParticleSystem> Particles;

	}
