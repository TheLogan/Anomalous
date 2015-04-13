using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour {

    public AudioClip start;
    public AudioClip loop;
    [SerializeField]private AudioSource aSource;
    private bool playing = false;


    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

	void Update () 
    {
        if (playing && !aSource.isPlaying)
        {
            aSource.clip = loop;
            aSource.loop = true;
            aSource.Play();
        }

	}

    public void Play()
    {
        if (!playing)
        {
            if (start != null)
            { 
                aSource.loop = false;
                aSource.clip = start;
                playing = true;
            }
            else
            {
                aSource.loop = true;
                aSource.clip = loop;
                playing = true;
            }

            aSource.Play();
        }

    }

    public void Stop(bool abrupt)
    {
        if (playing)
        {
            if (abrupt)
            {
                aSource.Stop();
                playing = false;
            }
            else
            {
                aSource.loop = false;
                playing = false;
            }
        }
    }

    public void DelayStop(float waitTime)
    {
        StartCoroutine(DelayStopE(waitTime));

    }

    IEnumerator DelayStopE(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        aSource.Stop();
        playing = false;
    }
}
