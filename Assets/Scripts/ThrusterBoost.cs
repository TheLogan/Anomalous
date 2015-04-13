using UnityEngine;
using System.Collections;

public class ThrusterBoost : MonoBehaviour {

    public KeyCode activetKey;
    private float timer;

    Inventory inventory;
    SpaceShip spaceShip;

	[SerializeField] private AudioSource thrusterSource;

	// Use this for initialization
	void Start () {
        inventory = this.gameObject.GetComponent<Inventory>();
        spaceShip = this.gameObject.GetComponent<SpaceShip>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime;

        if (spaceShip.isBoosted && timer + inventory.boostDuration < inventory.boostCooldown )
        {
	        thrusterSource.pitch = 1f;
            spaceShip.isBoosted = false;
        }

        if (inventory.boostConsumables > 0 && timer <= 0 && Input.GetKeyDown(activetKey))
        {
	        thrusterSource.pitch = 2f;
            inventory.blinkConsumables--;
            timer = inventory.boostCooldown;
            spaceShip.isBoosted = true;
        }
	}
}
