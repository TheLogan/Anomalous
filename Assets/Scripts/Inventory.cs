using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ArtifactType {Blink, Battery, Overdrive}

[RequireComponent(typeof(CenterStats))]
public class Inventory : MonoBehaviour {


    public int baseCargoHoldSpace;
    public int baseEnergy;
    public float baseThrusterPower;

    public int blinkConsumables;
    public int boostConsumables;
    public int energyConsumables;

    public float blinkCooldown;
    public float boostCooldown;
    public float energyCooldown;
    public int energyConsumablePower;
    public int boostConsumablePower;
    public int blinkConsumablePower;
    public int boostDuration;

    public int scrap = 0;
    public int money = 0;
    public List<ArtifactType> artifacts = new List<ArtifactType>();
    public int pickUpLayer;
    private CenterStats cs;

	[SerializeField] private GameObject batteryFab;
	[SerializeField] private GameObject blinkFab;
	[SerializeField] private GameObject overdriveFab;
	
    // Use this for initialization
	void Start () 
    {
        cs = GetComponent<CenterStats>();
	}
	

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == pickUpLayer)
        {
	        if (other.gameObject.GetComponent<ScrapItem>() != null)
	        {
		        if (scrap < baseCargoHoldSpace)
		        {
					other.gameObject.GetComponent<PickupableItem>().PickupItem(this);
					cs.pullingObject.Remove(other.rigidbody);
					Destroy(other.gameObject);
		        }
	        }
	        else
	        {
				other.gameObject.GetComponent<PickupableItem>().PickupItem(this);
				cs.pullingObject.Remove(other.rigidbody);
				Destroy(other.gameObject);
	        }
        }
    }
    

    public void AddScrap(int amount)
    {
        scrap += amount;
        if (scrap > baseCargoHoldSpace)
        {
            scrap = baseCargoHoldSpace;
        }
    }


	public void DropInventory()
	{
		Object[] allPrefabs = Resources.LoadAll("WorldGenFabs");

		var world = GameObject.Find("Anomalous world GO").GetComponent<WorldController>();

		if (artifacts.Contains(ArtifactType.Battery))
		{
			var go = (GameObject)Instantiate(batteryFab, transform.position, Quaternion.identity);
			world.GoListAdd(go);
		}
		if (artifacts.Contains(ArtifactType.Blink))
		{
			var go = (GameObject)Instantiate(blinkFab, transform.position, Quaternion.identity);
			world.GoListAdd(go);
		}
		if (artifacts.Contains(ArtifactType.Overdrive))
		{
			var go = (GameObject)Instantiate(overdriveFab, transform.position, Quaternion.identity);
			world.GoListAdd(go);
		}
		artifacts.Clear();
		scrap = 0;
	}


    public void AddArtifact(ArtifactType artifactType)
    {
        artifacts.Add(artifactType);
    }
}
