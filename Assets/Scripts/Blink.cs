using System.Runtime.Serialization.Formatters;
using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour 
{

    private Inventory inventory;
    private float timer;

    public KeyCode activetKey;
    public Camera cam;
    public ParticleSystem ps;
    private bool LastFramePlay = false;

	[SerializeField] private AudioSource source;
	[SerializeField] private AudioClip boostClip;

    void Start()
    {
        inventory = this.gameObject.GetComponent<Inventory>();
        timer = inventory.blinkCooldown;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (inventory.blinkConsumables > 0 && timer <= 0 && Input.GetKeyDown(activetKey))
        {
            inventory.blinkConsumables--;
            timer = inventory.blinkCooldown;
            ps.Play();

	        source.clip = boostClip;
			source.Play();
        }
		
        if (LastFramePlay && !ps.isPlaying)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = (cam.transform.position.z * -1) - transform.position.z;
            pos = cam.ScreenToWorldPoint(pos);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
			source.Stop();
        }



        LastFramePlay = ps.isPlaying;

    }


}
