using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour {

    public Image image;
    public GameObject button;
    public GameObject player;
    public Text resourceText;

    protected Inventory inventory;
    protected DescriptionScript descriptionScript;

    public string describtionText;
    public int upgradeCost;
    public float power;
    public int level;
    public bool upgradeable = true;



    void Start()
    {
       
    }

    void OnAwake()
    {

    }

    public virtual void UpdateUpgradeInfo()
    {

    }

    public void Upgrade()
    {
        if (inventory.money >= upgradeCost)
        {
            inventory.money -= upgradeCost;
            level++;
            UpdateUpgradeInfo();
            UpdatePlayer();
            UpdateDescription();
            resourceText.text = "Resources: " + inventory.money.ToString();
        }
    }

    public virtual void UpdatePlayer()
    {

    }

    public virtual void UpdateDescription()
    {

    }
}
