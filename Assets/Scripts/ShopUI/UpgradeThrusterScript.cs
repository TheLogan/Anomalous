using UnityEngine;
using System.Collections;

public class UpgradeThrusterScript : UpgradeScript
{
	[SerializeField]
	private AudioClip buySoundClip;
	[SerializeField]
	private AudioSource mySource;

    // Use this for initialization
    void Start()
    {
        inventory = player.GetComponent<Inventory>();
        descriptionScript = image.GetComponent<DescriptionScript>();
        level = 1;
        describtionText = "Increases the power of your ships thruster.";
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
                upgradeCost = 123;
                power = 1;
                break;

            case 2:
                upgradeCost = 234;
                power = 1.1f;
                break;

            case 3:
                upgradeCost = 345;
                power = 1.3f;
                break;

            case 4:
                upgradeCost = 456;
                power = 1.7f;
                break;

            case 5:
                upgradeCost = 567;
                power = 2.2f;
                break;

            case 6:
                power = 3;
                upgradeable = false;
                button.SetActive(false);
                break;

            default:
                Debug.Log("Trying to upgrade Thrusters to a non exsisting level.");
                break;
        }
        image.fillAmount = (float)level / 6;
    }

    public override void UpdatePlayer()
    {
        inventory.baseThrusterPower = power;
    }

    public override void UpdateDescription()
    {
		descriptionScript.UpdateDescription(describtionText, upgradeCost, "Thruster Power", Mathf.RoundToInt(power), level);
    }
}
