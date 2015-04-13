using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceSoundScript : MonoBehaviour {

    public GameObject SoundSource;
    public float rotationSpeed = 2;
    public AudioClip[] ClipToUse;

    public float maxAppearInterval = 10;
    public float minAppearInterval = 5;

    private float ApperensTimer;
    private AudioSource aSource;

    void Start()
    {
        aSource = SoundSource.GetComponent<AudioSource>();
        ApperensTimer = Random.Range(minAppearInterval,maxAppearInterval);
    }


    void Update()
    {
        if (ApperensTimer >= 0 && !aSource.isPlaying)
        {
            AudioClip clip = ClipToUse[Random.Range(0, ClipToUse.Length)];
            if (aSource.clip != clip)
            {
                aSource.clip = clip;
                aSource.Play();
                ApperensTimer = Random.Range(minAppearInterval, maxAppearInterval);
            }
        }
        else if(!aSource.isPlaying)
            ApperensTimer -= Time.deltaTime;


        transform.RotateAround(Vector3.zero, Vector3.forward, rotationSpeed * Time.deltaTime);

    }

}
