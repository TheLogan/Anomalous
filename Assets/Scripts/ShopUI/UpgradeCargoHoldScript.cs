using UnityEngine;
using System.Collections;

public class UpgradeCargoHoldScript : UpgradeScript
{
	[SerializeField] private AudioClip buySoundClip;
	[SerializeField] private AudioSource mySource;

    // Use this for initialization
    void Start()
    {
        inventory = player.GetComponent<Inventory>();
        descriptionScript = image.GetComponent<DescriptionScript>();
        level = 1;
        describtionText = "Increases the amount of cargo your ship can carry.";
        UpdateUpgradeInfo();
        UpdateDescription();
        UpdatePlayer();
    }

    public override void UpdateUpgradeInfo()
    {
		mySource.clip = buySoundClip;
		mySource.Play();
        switch (level)
        {
            case 1:
                upgradeCost = 50;
                power = 100;
                break;

            case 2:
                upgradeCost = 100;
                power = 275;
                break;

            case 3:
                upgradeCost = 200;
                power = 500;
                break;

            case 4:
                upgradeCost = 300;
                power = 800;
                break;

            case 5:
                upgradeCost = 400;
                power = 1200;
                break;

            case 6:
                power = 2000;
                upgradeable = false;
                button.SetActive(false);
                break;

            default:
                Debug.Log("Trying to upgrade Cargo Hold to a non exsisting level.");
                break;
        }
        image.fillAmount = (float)level / 6;
    }

    public override void UpdatePlayer()
    {
        inventory.baseCargoHoldSpace = Mathf.RoundToInt(power);
    }

    public override void UpdateDescription()
    {
        descriptionScript.UpdateDescription(describtionText, upgradeCost, "Cargo Space", Mathf.RoundToInt(power), level);
    }
}
