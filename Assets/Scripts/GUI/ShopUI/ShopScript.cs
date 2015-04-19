using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    private Inventory inventory;

    [SerializeField]
    private GameObject canvas;

    public int scrapValue = 1;

	private float coolDown = 0;


    [SerializeField]
    private Text BlinkArtifactText;
    [SerializeField]
    private Text BatteryArtifactText;
    [SerializeField]
    private Text OverdriveArtifactText;

    [SerializeField]
    private Image BlinkArtifactImage;
    [SerializeField]
    private Image BatteryArtifactImage;
    [SerializeField]
    private Image OverdriveArtifactImage;

    [SerializeField]
    private Text BlinkConsumableDescriptionText;
    [SerializeField]
    private Text EnergyConsumableDescriptionText;
    [SerializeField]
    private Text BoostConsumableDescriptionText;

    [SerializeField]
    private Button BuyBlinkConsumableButton;
    [SerializeField]
    private Image BlinkConsumableImage;
    [SerializeField]
    private Button BuyBoostConsumableButton;
    [SerializeField]
    private Image BoostConsumableImage;
    [SerializeField]
    private Button BuyEnergyConsumableButton;
    [SerializeField]
    private Image EnergyConsumableImage;

	[SerializeField]private Image BlinkUIImage;
	[SerializeField] private Text BlinkUIText;
	[SerializeField]
	private Image BoostUIImage;
	[SerializeField]
	private Text BoostUIText;
	[SerializeField]
	private Image EnergyUIImage;
	[SerializeField]
	private Text EnergyUIText;

    [SerializeField]
    private Text resourceText;

    [SerializeField]
    private AudioClip rechargeClip;
    [SerializeField]
    private AudioClip buySoundClip;
    [SerializeField]
    private AudioSource mySource;

    public int blinkConsumableCost;
    public int boostConsumableCost;
    public int energyConsumableCost;


    // Use this for initialization
    void Start()
    {
        inventory = player.GetComponent<Inventory>();

        BlinkConsumableDescriptionText.text = "Consumable: Blink" + System.Environment.NewLine
        + "Instant travel to a nearby location." + System.Environment.NewLine
        + "Cost: " + blinkConsumableCost + System.Environment.NewLine
        + "Hotkey: 1" + System.Environment.NewLine
        + "Cooldown: " + inventory.blinkCooldown;

        EnergyConsumableDescriptionText.text = "Consumable: Energy" + System.Environment.NewLine
        + "Instant recharging of " + inventory.energyConsumablePower + " energy." + System.Environment.NewLine
        + "Cost: " + energyConsumableCost + System.Environment.NewLine
        + "Hotkey: 3" + System.Environment.NewLine
        + "Cooldown: " + inventory.energyCooldown;

        BoostConsumableDescriptionText.text = "Consumable: Boost" + System.Environment.NewLine
        + "Temporary boost to thrusters." + System.Environment.NewLine
        + "Cost: " + boostConsumableCost + System.Environment.NewLine
        + "Hotkey: 2" + System.Environment.NewLine
        + "Cooldown: " + inventory.boostCooldown;

    }

    // Update is called once per frame
    void Update()
    {
	    if (coolDown > 0)
	    {
		    coolDown -= Time.deltaTime;
	    }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && coolDown <= 0)
        {
            mySource.clip = rechargeClip;
            mySource.Play();
            Debug.Log("Collision with the base.");
            SellCargo();
            player.GetComponent<EnergyCore>().ResetPower();

            GlobalSettings.TogglePause(true);
            canvas.SetActive(true);
        }
    }

    public void ExitShop()
    {
	    coolDown = 1f;
        GlobalSettings.TogglePause(false);
        canvas.SetActive(false);
    }

    public void OnClickTest()
    {
        Debug.Log("Button is clicked!");
    }

    public void SellCargo()
    {
        inventory.money += inventory.scrap * scrapValue;
        inventory.scrap = 0;

        foreach (ArtifactType type in inventory.artifacts)
        {
            switch (type)
            {
                case ArtifactType.Blink:
                    BlinkArtifactText.text = "Enables you to warp space time, instantly teleporting to a different location.";
                    BlinkArtifactImage.color = Color.white;
					BlinkConsumableImage.gameObject.SetActive(true);
					BuyBlinkConsumableButton.gameObject.SetActive(true);
					BlinkUIImage.gameObject.SetActive(true);
					BlinkUIText.gameObject.SetActive(true);
                    //Enable consumeable Canvas
                    break;
                case ArtifactType.Battery:
                    BatteryArtifactText.text = "Unknown technology that allows the compression of energy into a miniscule object.";
                    BatteryArtifactImage.color = Color.white;
					EnergyConsumableImage.gameObject.SetActive(true);
					BuyEnergyConsumableButton.gameObject.SetActive(true);
					EnergyUIImage.gameObject.SetActive(true);
					EnergyUIText.gameObject.SetActive(true);
                    break;
                case ArtifactType.Overdrive:
                    OverdriveArtifactText.text = "Allows rapid acceleration boosts at the cost of extra fuel.";
                    OverdriveArtifactImage.color = Color.white;
					BoostConsumableImage.gameObject.SetActive(true);
					BuyBoostConsumableButton.gameObject.SetActive(true);
					BoostUIImage.gameObject.SetActive(true);
					BoostUIText.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        inventory.artifacts.Clear();

        resourceText.text = "Resources: " + inventory.money.ToString();
    }

    public void BuyBlinkConsumable()
    {
        if (inventory.money >= blinkConsumableCost)
        {
            inventory.blinkConsumables++;
            inventory.money -= blinkConsumableCost;
            mySource.clip = buySoundClip;
            mySource.Play();
			resourceText.text = "Resources: " + inventory.money;

        }
    }

    public void BuyBoostConsumable()
    {
        if (inventory.money >= boostConsumableCost)
        {
            inventory.boostConsumables++;
            inventory.money -= boostConsumableCost;
            mySource.clip = buySoundClip;
            mySource.Play();
			resourceText.text = "Resources: " + inventory.money;
        }
    }

    public void BuyEnergyConsumable()
    {
        if (inventory.money >= energyConsumableCost)
        {
            inventory.energyConsumables++;
            inventory.money -= energyConsumableCost;
            mySource.clip = buySoundClip;
            mySource.Play();
			resourceText.text = "Resources: " + inventory.money;
        }
    }
}
